using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.UseCases.Experiences
{
    public class UpdateExperience
    {
        private readonly IExperienceRepository _repository;

        public UpdateExperience(IExperienceRepository repository)
        {
            _repository = repository;
        }

        public void Execute(Experience experience)
        {
            var existingExperience = _repository.GetById(experience.Id);
            if (existingExperience == null)
            {
                throw new ArgumentException("Experience not found.");
            }
            if (experience.StartDate > DateTime.Now)
            {
                throw new ArgumentException("Start date cannot be in the future.");
            }
            if (experience.EndDate.HasValue && experience.EndDate < experience.StartDate)
            {
                throw new ArgumentException("End date cannot be earlier than start date.");
            }
            _repository.Update(experience);
        }
    }
}
