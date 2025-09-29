using Portfolio.Application.DTOs.VersionCvs;
using Portfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.DTOs.Users
{
    public static class UserMapper
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                VersionsCV = user.VersionCVs.Select(x => x.ToDto()).ToList()
            };
        }

        public static User ToEntity(this CreateUserDto dto)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };
        }

        public static User ToEntity(this UpdateUserDto dto)
        {
            return new User
            {
                Id = dto.Id,
                Name = dto.Name
            };
        }
    }
}
