using DepositApi.DAL.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace DepositApi.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> dbSet;
        private readonly AppDbContext context;

        public Repository(AppDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public async Task CreateAsync(T item)
        {
            await this.dbSet.AddAsync(item);
            await this.context.SaveChangesAsync();
        }

        public async Task CreateRangeAsync(IEnumerable<T> items)
        {
            await this.dbSet.AddRangeAsync(items);
            await this.context.SaveChangesAsync();
        }

        public async Task<T> FindAsync(int id)
        {
            return await this.dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindRangeAsync(Expression<Func<T, bool>> predicate, int startIndex, int count)
        {
            return await this.dbSet.Where(predicate).Skip(startIndex).Take(count).ToListAsync();
        }

        public async Task UpdateAsync(T item)
        {
            this.dbSet.Update(item);
            await this.context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            T entity = await dbSet.FindAsync(id);
            this.dbSet.Remove(entity);
            await this.context.SaveChangesAsync();
        }
    }
}
