using Portfolio.Application.DTOs.VersionCvs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.DTOs.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public List<VersionCVDto> VersionsCV { get; set; } = new();
    }
}
