using ConcertBookingApp.Data.Database;
using ConcertBookingApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.Data.Repositorys
{
    public class PerformanceRepository
    {
        private readonly ApplicationDbContext _applicationDb;

        public PerformanceRepository(ApplicationDbContext applicationDb)
        {
            _applicationDb = applicationDb;
        }
        //public async Task<Performance> Findperformance()
        //{
        //    return await _applicationDb.Performances
        //}
    }
}
