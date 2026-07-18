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

    public static UserResponse ResponseFromEntity(UserEntity user)
    {
        return new UserResponse
        {
            Id = user.Id,
            EmailConfirmed = user.EmailConfirmed,
            Email = user.Email ?? "",
            LastName = user.LastName,
            Name = user.FirstName
        };
    }
}