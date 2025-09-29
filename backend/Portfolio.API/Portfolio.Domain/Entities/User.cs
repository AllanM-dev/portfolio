using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Entities
{
    public class User
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public ICollection<VersionCV> VersionCVs{ get; set; } = new List<VersionCV>();
    }
}
