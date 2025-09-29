using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Entities
{
    public class VersionCV
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
