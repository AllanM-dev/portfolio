using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.DTOs.Experiences
{
    public class CreateExperienceDto
    {
        public string Title { get; set; } = "";
        public string Company { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; } = "";
        public List<string> Skills { get; set; } = new();
    }
}
