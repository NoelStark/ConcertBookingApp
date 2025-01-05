using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConcertBookingApp.Data.Repositorys;
namespace ConcertBookingApp.Data.Database
{
    public interface IUnitOfWork : IDisposable
    {
        IPerformanceRepository Performance { get; }
        IBookingPerformanceRepository BookingPerformance { get; }
        IBookingRepository Booking {  get; }
        IConcertRepository Concert { get; }
        Task Save();

    }
}
