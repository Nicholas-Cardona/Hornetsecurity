using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hornet.Data.Models;
using Hornet.Domain.DTOs.Account;

namespace Hornet.Data.Mappers;

public static class UserMapper
{
    public static User FromRequest(SignUpRequest request)
    {
        return new User
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Password = request.Password
        };
    }
}