using ConcertBookingApp.Data.Database;
using Microsoft.EntityFrameworkCore;
using SharedResources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.Data.Repositorys
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        { 
        }

        public async Task<User> FindUser(string fullname, string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(a => a.Name.ToLower() == fullname.ToLower() && a.Email.ToLower() == email.ToLower()) ;
        }

        public async Task<int> AddUser(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user.UserId;
        }
    }
}
