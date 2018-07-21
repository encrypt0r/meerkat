using Meerkat.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Meerkat.Web.Repositories
{
    public interface IRepository<T, TId> where TId : struct, IComparable
    {
        void Add(T item);
        void Remove(T item);

        Task<ICollection<T>> GetAllAsync();
        Task<T> Get(TId id);
        Task<ICollection<Event>> Get(Expression<Func<T, bool>> predicate);
    }
}
