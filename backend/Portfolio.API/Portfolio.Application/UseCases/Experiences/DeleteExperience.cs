using Portfolio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.UseCases.Experiences
{
    public class DeleteExperience
    {
        private readonly IExperienceRepository _repository;

        public DeleteExperience(IExperienceRepository repository)
        {
            _repository = repository;
        }

        public void Execute(int id)
        {
            var experience = _repository.GetById(id);
            if (experience == null)
            {
                throw new ArgumentException("Experience not found.");
            }
            _repository.Delete(id);
        }
    }
}
