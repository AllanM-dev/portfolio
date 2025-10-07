using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.DTOs.Experiences
{
    public class CreateExperienceDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [MinLength(1, ErrorMessage = "Title must not be empty.")]
        public string Title { get; set; } = "";

        [Required]
        public string Company { get; set; } = "";

        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; } = "";
        public List<string>? Skills { get; set; } = new();
    }
}
