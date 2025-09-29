using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.UseCases.Experiences
{
    public class GetExperienceById
    {
        private readonly IExperienceRepository _repository;

        public GetExperienceById(IExperienceRepository repository)
        {
            _repository = repository;
        }

        public Experience? Execute(int id)
        {
            return _repository.GetById(id);
        }
    }
}
