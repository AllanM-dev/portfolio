using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.UseCases.Users
{
    public class GetUserById
    {
        private readonly IUserRepository _repository;

        public GetUserById(IUserRepository repository)
        {
            _repository = repository;
        }

        public User? Execute(Guid userId)
        {
            return _repository.GetById(userId);
        }
    }
}
