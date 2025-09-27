using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.UseCases
{
    public class AddExperience
    {
        private readonly IExperienceRepository _repository;

        public AddExperience(IExperienceRepository repository)
        {
            _repository = repository;
        }

        public void Execute(Experience experience)
        {
            if (experience.StartDate > DateTime.Now)
            {
                               throw new ArgumentException("Start date cannot be in the future.");
            }
            _repository.Add(experience);
        }
    }
}
