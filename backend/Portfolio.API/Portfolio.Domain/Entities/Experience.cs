using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Entities
{
    public class Experience
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Company { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }
        public List<string>? Skills { get; set; } = new();
    }
}
