using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Interfaces
{
    public interface IRepository<T> where T : IModelBase
    {
        ValueTask<T> GetAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity, bool saveChanges = false);
        Task RemoveAsync(T entity, bool saveChanges = false);
        Task<int> SaveChangesAsync();
    }
}
