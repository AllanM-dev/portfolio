using Portfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Interfaces
{
    public interface IVersionCVRepository
    {
        VersionCV? GetById(int id);
        void Add(VersionCV version);
        void Update(VersionCV version);
        void Delete(int id);
        IEnumerable<VersionCV> GetByUserId(Guid userId);
    }
}
