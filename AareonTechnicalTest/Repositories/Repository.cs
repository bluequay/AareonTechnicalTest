using AareonTechnicalTest.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Repositories
{
    public class Repository<T> : IRepository<T> where T: class, IModelBase
    {
        protected readonly ApplicationContext _context;
        
        public Repository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity, bool saveChanges)
        {
            await _context.Set<T>().AddAsync(entity);

            if(saveChanges)
            {
                await SaveChangesAsync();
            }
        }
      
        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async ValueTask<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task RemoveAsync(T entity, bool saveChanges)
        {
            _context.Set<T>().Remove(entity);

            if (saveChanges)
            {
                await SaveChangesAsync();
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
