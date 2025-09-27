using Portfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Interfaces
{
    public interface IExperienceRepository
    {
        IEnumerable<Experience> GetExperiences();
        Experience? GetById(int id);
        void Add(Experience experience);
        void Update(Experience experience);
        void Delete(int id);
    }
}
