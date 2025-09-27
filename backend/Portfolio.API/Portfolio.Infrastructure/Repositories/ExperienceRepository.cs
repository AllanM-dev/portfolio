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
    public class ExperienceRepository : IExperienceRepository
    {
        private readonly PortfolioDbContext _context;

        public ExperienceRepository(PortfolioDbContext context)
        {
            _context = context;
        }

        public void Add(Experience experience)
        {
            _context.Experiences.Add(experience);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var exp = _context.Experiences.Find(id);
            if (exp != null)
            {
                _context.Experiences.Remove(exp);
                _context.SaveChanges();
            }
        }

        public Experience? GetById(int id)
        {
            return _context.Experiences.AsNoTracking().FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Experience> GetExperiences()
        {
            return _context.Experiences.AsNoTracking().ToList();
        }

        public void Update(Experience experience)
        {
            _context.Experiences.Update(experience);
            _context.SaveChanges();
        }
    }
}
