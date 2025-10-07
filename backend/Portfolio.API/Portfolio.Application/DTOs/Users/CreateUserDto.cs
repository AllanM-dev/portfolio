using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.DTOs.Users
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(1, ErrorMessage = "Name cannot be empty.")]
        public string Name { get; set; } = "";
    }
}
