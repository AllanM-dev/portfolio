using Portfolio.Application.DTOs.Users;
using Portfolio.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.UseCases.Users
{
    public class GetUsers
    {
        private readonly IUserRepository _repository;

        public GetUsers(IUserRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<UserDto> Execute()
        {
            return _repository.GetUsers()
                .OrderByDescending(u => u.Name)
                .Select(u => u.ToDto());
        }
    }
}
