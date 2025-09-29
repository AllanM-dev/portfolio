using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.DTOs.Users
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
    }
}
