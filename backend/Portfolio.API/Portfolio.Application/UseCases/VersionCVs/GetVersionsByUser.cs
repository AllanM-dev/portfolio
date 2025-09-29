using Portfolio.Application.DTOs.VersionCvs;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.UseCases.VersionCVs
{
    public class GetVersionsByUser
    {
        private readonly IVersionCVRepository _repository;

        public GetVersionsByUser(IVersionCVRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<VersionCVDto> Execute(Guid userId)
        {
            return _repository.GetByUserId(userId)
                .OrderByDescending(v => v.Name)
                .Select(v => v.ToDto());
        }
    }
}
