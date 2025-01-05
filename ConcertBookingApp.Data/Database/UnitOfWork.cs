using ConcertBookingApp.Data.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConcertBookingApp.Data.Database
{
    public class UnitOfWork
    {
        private protected readonly ApplicationDbContext _dbContext;

        private IPerformanceRepository _performanceRepository;
        private IConcertRepository _concertRepository;
        private IBookingRepository _bookingRepository;
        private IBookingPerformanceRepository _bookingPerformanceRepository;
        public UnitOfWork(ApplicationDbContext applicationDb) 
        {
            _dbContext = applicationDb;
        }
        public IConcertRepository Concert { get => _concertRepository = new ConcertRepository(_dbContext);}
        public IPerformanceRepository Performance { get => _performanceRepository = new PerformanceRepository(_dbContext); }
        public IBookingRepository Booking { get => _bookingRepository = new BookingRepository(_dbContext); }
        public IBookingPerformanceRepository BookingPerformance { get => _bookingPerformanceRepository = new BookingPerformanceRepository(_dbContext); }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
        public async Task Save()
        {
           await _dbContext.SaveChangesAsync();
        }
    }
}
