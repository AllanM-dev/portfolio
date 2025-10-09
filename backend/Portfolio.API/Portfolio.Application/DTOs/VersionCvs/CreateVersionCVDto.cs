using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.DTOs.VersionCvs
{
    public class CreateVersionCVDto
    {
        [Required]
        [MinLength(1)]
        public string Name { get; set; } = "";

        [Required]
        public Guid UserId { get; set; }
    }
}
