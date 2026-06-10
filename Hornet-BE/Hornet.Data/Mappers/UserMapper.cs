using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hornet.Data.Entities;
using Hornet.Domain.DTOs.Account;

namespace Hornet.Data.Mappers;

public static class UserMapper
{
    public static UserEntity FromRequest(SignUpRequest request)
    {
        return new UserEntity
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Password = request.Password
        };
    }
}