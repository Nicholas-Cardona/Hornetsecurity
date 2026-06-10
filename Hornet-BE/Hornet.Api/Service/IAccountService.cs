using Hornet.Data.Entities;
using Hornet.Domain.DTOs.Account;

namespace Hornet.Api.Service;

public interface IAccountService
{
    Task SignUpUserAsync(SignUpRequest request);
    Task SignInUserAsync(SignInRequest request);
}