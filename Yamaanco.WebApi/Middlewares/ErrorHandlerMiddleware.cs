using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Yamaanco.Application.ApiResponses;
using Yamaanco.Application.Common.Exceptions;
using Yamaanco.Application.Enums;

namespace Yamaanco.WebApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { Succeeded = false, Message = error?.Message };

                switch (error)
                {
                    case YamaancoException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case ValidationException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        responseModel.ErrorMessages = e.Errors;
                        break;

                    case DeleteFailureException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case NotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case RequestedUserIsMemberOfGroupException e:
                        response.StatusCode = (int)YamaancoHttpStatusCode.RequestedUserIsMemberOfGroup;
                        break;
                    case UserNameAlreadyExistException e:
                        response.StatusCode = (int)YamaancoHttpStatusCode.UserNameAlreadyExist;
                        break;
                    case UserEmailAlreadyExistException e:
                        response.StatusCode = (int)YamaancoHttpStatusCode.UserEmailAlreadyExist;
                        break;
                    case UserPhoneNumberAlreadyExistException e:
                        response.StatusCode = (int)YamaancoHttpStatusCode.UserPhoneNumberAlreadyExist;
                        break;
                    case NotConfirmedAccountException e:
                        response.StatusCode = (int)YamaancoHttpStatusCode.NotConfirmedAccount;
                        break;
                    case InvalidCredentialsException e:
                        response.StatusCode = (int)YamaancoHttpStatusCode.InvalidCredentials;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}