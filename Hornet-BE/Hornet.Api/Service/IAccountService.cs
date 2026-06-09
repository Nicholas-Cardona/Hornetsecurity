using Hornet.Data.Entities;
using Hornet.Domain.DTOs.Account;

namespace Hornet.Api.Service;

public interface IAccountService
{
    Task<UserEntity> SignUpUserAsync(SignUpRequest request);
    Task<UserEntity> SignInUserAsync(SignInRequest request);
}