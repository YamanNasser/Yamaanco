using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Common.Exceptions;
using Yamaanco.Application.Common.Options;
using Yamaanco.Application.DTOs.Account;
using Yamaanco.Application.DTOs.Authentication;
using Yamaanco.Application.DTOs.SystemNotifications;
using Yamaanco.Application.Enums;
using Yamaanco.Application.Interfaces;
using Yamaanco.Domain.ValueObjects;
using Yamaanco.Infrastructure.EF.Identity.Persistence.Helper;
using Yamaanco.Infrastructure.EF.Identity.Persistence.Models;

namespace Yamaanco.Infrastructure.EF.Identity.Persistence.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly INotificationService _emailService;
        private readonly JwtOptions _jwtSettings;
        private readonly IDateTime _dateTimeService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IOptions<EmailOptions> _emailSettings;

        public AccountService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<JwtOptions> jwtSettings,
            IDateTime dateTimeService,
            SignInManager<ApplicationUser> signInManager,
            INotificationService emailService,
            IOptions<EmailOptions> emailSettings,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _dateTimeService = dateTimeService;
            _signInManager = signInManager;
            _emailService = emailService;
            _emailSettings = emailSettings;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task UpdateAccount(string id, string firstName, string lastName, string email, string phoneNumber)
        {
            var userName = (new UserName(firstName, lastName)).ToString();

            var isCurrentNameIsExist = _userManager.Users.Where(u => u.UserName.Equals(userName) && u.Id != id).Any();

            if (isCurrentNameIsExist)
            {
                throw new UserNameAlreadyExistException(userName);
            }

            var isCurrentEmailIsExist = _userManager.Users.Where(u => u.Email.Equals(email) && u.Id != id).Any();

            if (isCurrentEmailIsExist)
            {
                throw new UserEmailAlreadyExistException(email);
            }

            var isCurrentPhoneNumberIsExist = _userManager.Users.Where(u => u.PhoneNumber.Equals(phoneNumber) && u.Id != id).Any();

            if (isCurrentPhoneNumberIsExist)
            {
                throw new UserPhoneNumberAlreadyExistException(phoneNumber);
            }

            var userInfo = await _userManager.Users.SingleOrDefaultAsync(o => o.Id == id);

            if (userInfo != null)
            {
                userInfo.FirstName = firstName;
                userInfo.LastName = lastName;
                userInfo.UserName = userName;
                userInfo.PhoneNumber = phoneNumber;
                userInfo.Email = email;
            }

            await _userManager.UpdateAsync(userInfo);
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser), request.Email);
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new InvalidCredentialsException(request.Email);
            }
            if (!user.EmailConfirmed)
            {
                throw new NotConfirmedAccountException(request.Email);
            }
            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
            AuthenticationResponse response = new AuthenticationResponse
            {
                Id = user.Id,
                JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName
            };

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            var refreshToken = GenerateRefreshToken(ipAddress);
            response.RefreshToken = refreshToken.Token;
            return new Response<AuthenticationResponse>(response, $"Authenticated {user.UserName}");
        }

        public async Task<Response<RegisterResponse>> RegisterAsync(RegisterRequest request, string origin)
        {
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                throw new UserNameAlreadyExistException(request.UserName);
            }

            var isCurrentPhoneNumberIsExist = _userManager.Users.Where(u => u.PhoneNumber.Equals(request.PhoneNumber)).Any();

            if (isCurrentPhoneNumberIsExist)
            {
                throw new UserPhoneNumberAlreadyExistException(request.PhoneNumber);
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                    var verification = await SendVerificationEmail(user, origin);
                    await _emailService.SendAsync(new SingleEmail()
                    {
                        Body = $"Please confirm your account by visiting this URL {verification.VerificationUri}",

                        Subject = "Confirm Registration",
                        To = user.Email
                    }, _emailSettings.Value);
                    return new Response<RegisterResponse>(new RegisterResponse()
                    { UserId = user.Id, Code = verification.Code, Email = user.Email }, message: $"User Registered. Please confirm your account by visiting this URL {verification.VerificationUri}");
                }
                else
                {
                    throw new YamaancoException($"{result.Errors}");
                }
            }
            else
            {
                throw new UserEmailAlreadyExistException(request.Email);
            }
        }

        public User GetCurrentUser()
        {
            var userInfo = new User();
            foreach (var claim in _httpContextAccessor.HttpContext.User.Claims)
            {
                switch (claim.Type)
                {
                    case "firstName":
                        userInfo.FirstName = claim.Value;
                        break;

                    case "lastName":
                        userInfo.LastName = claim.Value;
                        break;

                    case ClaimTypes.Email:
                        userInfo.Email = claim.Value;
                        break;

                    case ClaimTypes.NameIdentifier:
                        userInfo.Id = claim.Value;
                        break;

                    case ClaimTypes.MobilePhone:
                        userInfo.PhoneNumber = claim.Value;
                        break;

                    case "ip":
                        userInfo.IP = claim.Value;
                        break;
                }
            }
            return userInfo;
        }

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
            }

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim("ip", ipAddress)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private class VerificationEmailResponse
        {
            public string VerificationUri { get; set; }
            public string Code { get; set; }
        }

        private async Task<VerificationEmailResponse> SendVerificationEmail(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "api/account/confirm-email/";
            var _enpointUri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);
            //Email Service Call Here
            return new VerificationEmailResponse() { Code = code, VerificationUri = verificationUri };
        }

        public async Task<Response<string>> ConfirmEmailAsync(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return new Response<string>(user.Id, message: $"Account Confirmed for {user.Email}. You can now use the /api/Account/authenticate endpoint.");
            }
            else
            {
                throw new YamaancoException($"An error occurred while confirming {user.Email}.");
            }
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }
    }
}