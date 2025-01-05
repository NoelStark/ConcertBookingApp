using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.Data.Repositorys
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        Task Add(TEntity entity);
        Task Delete(TEntity entity);
        Task Update(TEntity entity);

        Task<IEnumerable<TEntity>> GetAll();
    }
}
