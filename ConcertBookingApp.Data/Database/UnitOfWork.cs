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
        private readonly ApplicationDbContext _applicationDb;
        public UnitOfWork(ApplicationDbContext applicationDb) 
        {
            _applicationDb = applicationDb;
        }

        public void Dispose()
        {
            _applicationDb.Dispose();
        }

        public void Save()
        {
            _applicationDb.SaveChanges();
        }

        public ApplicationDbContext DatabaseConnection()
        {
            return _applicationDb;
        }


    }
}
