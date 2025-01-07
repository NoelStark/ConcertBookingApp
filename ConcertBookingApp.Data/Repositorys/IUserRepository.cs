using SharedResources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.Data.Repositorys
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> FindUser(string firstName, string email);
        Task<int> AddUser(User user);
    }
}
