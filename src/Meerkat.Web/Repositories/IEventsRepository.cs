using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Meerkat.Web.Models;

namespace Meerkat.Web.Repositories
{
    public interface IEventsRepository
    {
        void Add(Event item);
        Task<ICollection<Event>> Get(Expression<Func<Event, bool>> predicate);
        Task<Event> Get(long id);
        Task<ICollection<Event>> GetAllAsync();
        void Remove(Event item);
    }
}