using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.UseCases.VersionCVs
{
    public class GetVersionCVById
    {
        private readonly IVersionCVRepository _repository;
        public GetVersionCVById(IVersionCVRepository repository)
        {
            _repository = repository;
        }
        public VersionCV? Execute(int versionId)
        {
            return _repository.GetById(versionId);
        }
    }
}
