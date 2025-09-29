using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.UseCases.VersionCVs
{
    public class AddVersionCV
    {
        private readonly IVersionCVRepository _repository;

        public AddVersionCV(IVersionCVRepository repository)
        {
            _repository = repository;
        }

        public void Execute(VersionCV version)
        {
            _repository.Add(version);
        }
    }
}
