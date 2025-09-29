using Portfolio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User? GetById(Guid id);
        void Add(User user);
        void Update(User user);
        void Delete(Guid id);
    }
}
