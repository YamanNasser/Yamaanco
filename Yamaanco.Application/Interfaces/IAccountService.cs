using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.DTOs.Account;
using Yamaanco.Application.DTOs.Authentication;

namespace Yamaanco.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);

        Task<Response<RegisterResponse>> RegisterAsync(RegisterRequest request, string origin);

        Task<Response<string>> ConfirmEmailAsync(string userId, string code);

        User GetCurrentUser();

        Task UpdateAccount(string id, string firstName, string lastName, string email, string phoneNumber);
    }
}