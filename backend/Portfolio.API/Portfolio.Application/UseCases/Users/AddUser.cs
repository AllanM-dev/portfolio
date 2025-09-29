using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.UseCases.Users
{
    public class AddUser
    {
        private readonly IUserRepository _repository;

        public AddUser(IUserRepository repository)
        {
            _repository = repository;
        }

        public void Execute(User user)
        {
            _repository.Add(user);
        }
    }
}
