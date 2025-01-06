using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources.Models;

namespace ConcertBookingApp.Services
{
    public class UserService
    {
        public User? CurrentUser { get; set; } = null;
    }
}
