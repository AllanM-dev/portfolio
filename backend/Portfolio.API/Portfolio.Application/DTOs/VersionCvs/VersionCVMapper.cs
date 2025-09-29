using Portfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.DTOs.VersionCvs
{
    public static class VersionCVMapper
    {
        public static VersionCVDto ToDto(this VersionCV version)
        {
            return new VersionCVDto
            {
                Id = version.Id,
                Name = version.Name,
                UserId = version.UserId
            };
        }

        public static VersionCV ToEntity(this CreateVersionCVDto dto)
        {
            return new VersionCV
            {
                Name = dto.Name,
                UserId = dto.UserId
            };
        }

        public static VersionCV ToEntity(this UpdateVersionCVDto dto)
        {
            return new VersionCV
            {
                Id = dto.Id,
                Name = dto.Name,
                UserId = dto.UserId
            };
        }
    }
}
