using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.DTOs.VersionCvs
{
    public class VersionCVDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public Guid UserId { get; set; }
    }
}
