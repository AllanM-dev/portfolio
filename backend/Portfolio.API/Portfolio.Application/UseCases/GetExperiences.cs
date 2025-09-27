using Portfolio.Application.DTOs.Experiences;
using Portfolio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.UseCases
{
    public class GetExperiences
    {
        private readonly IExperienceRepository _repository;

        public GetExperiences(IExperienceRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ExperienceDto> Execute()
        {
            return _repository.GetExperiences()
                .OrderByDescending(e => e.StartDate)
                .Select(e => e.ToDto());
        }
    }
}
