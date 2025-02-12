﻿using ConcertBookingApp.Data.Database;
using SharedResources.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.Data.Repositorys
{
    public class PerformanceRepository : GenericRepository<Performance>, IPerformanceRepository
    {
        public PerformanceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<Performance> FindPerformance(int performanceId)
        {
            return await _dbContext.Performances.FirstOrDefaultAsync(a => a.PerformanceId == performanceId);
        }

        public async Task RemoveSelectedPerformance(int performanceId)
        {
            Performance? performance = await _dbContext.Performances.FirstOrDefaultAsync(x => x.PerformanceId == performanceId);
            _dbContext.Performances.Remove(performance);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<Performance>> GetAllPerformances()
        {
            return await _dbContext.Performances.ToListAsync();
        }

        public async Task<List<Performance>> GetPerformancesForConcert(int concertId)
        {
            return _dbContext.Concerts.SelectMany(x => x.Performances).Where(x => x.ConcertId == concertId).ToList();
        }
        public async Task UpdateSeats(List<BookingPerformance> bookingPerformance)
        {
            foreach (var performance in bookingPerformance)
            {
                var foundPerformance = _dbContext.Performances.FirstOrDefault(x => x.PerformanceId == performance.PerformanceId);
                foundPerformance.AvailableSeats -= performance.SeatsBooked;
                _dbContext.Performances.Update(foundPerformance);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateSeatsForPerformance(int id, int quantity)
        {
            var performance = _dbContext.Performances.FirstOrDefault(x => x.PerformanceId == id);
            performance.AvailableSeats += quantity;
            await _dbContext.SaveChangesAsync();
        }
    }
}
