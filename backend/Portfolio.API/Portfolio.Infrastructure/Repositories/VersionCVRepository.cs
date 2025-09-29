using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Infrastructure.Repositories
{
    public class VersionCVRepository: IVersionCVRepository
    {
        private readonly PortfolioDbContext _context;

        public VersionCVRepository(PortfolioDbContext context)
        {
            _context = context;
        }

        public void Add(VersionCV version)
        {
            _context.VersionsCV.Add(version);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var version = _context.VersionsCV.Find(id);
            if (version != null)
            {
                _context.VersionsCV.Remove(version);
                _context.SaveChanges();
            }
        }

        public VersionCV? GetById(int id)
        {
            return _context.VersionsCV
                .Include(v => v.User)
                .AsNoTracking()
                .FirstOrDefault(v => v.Id == id);
        }

        public IEnumerable<VersionCV> GetByUserId(Guid userId)
        {
            return _context.VersionsCV
                .Where(v => v.UserId == userId)
                .Include(v => v.User)
                .AsNoTracking()
                .ToList();
        }

        public void Update(VersionCV version)
        {
            _context.VersionsCV.Update(version);
            _context.SaveChanges();
        }
    }
}
