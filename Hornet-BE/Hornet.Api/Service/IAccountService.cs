using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hornet.Data.Models;
using Hornet.Domain.DTOs.Account;

namespace Hornet.Api.Service;

public interface IAccountService
{
    Task<User> SignUpUserAsync(SignUpRequest request);
    Task<User> SignInUserAsync(SignInRequest request);
}