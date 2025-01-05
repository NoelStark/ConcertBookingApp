using SharedResources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.Data.Repositorys
{
    public interface IConcertRepository : IGenericRepository<Concert>
    {
        Task<List<Concert>> GetAllConcerts();
        
        Task<Concert> GetConcertForPerformance(int performanceId);
    }
}
