using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DepositApi.DAL.Repository
{
    public interface IRepository<T> where T : class
    {
        public Task CreateAsync(T item);

        public Task CreateRangeAsync(IEnumerable<T> items);

        public Task<T> FindAsync(int id);

        public Task<IEnumerable<T>> FindRangeAsync(Expression<Func<T, bool>> predicate, int startIndex, int count);

        public Task UpdateAsync(T item);

        public Task Delete(int id);
    }
}
