using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Meerkat.Web.Models;

namespace Meerkat.Web.Repositories
{
    public interface IEventsRepository : IRepository<Event, long>
    {

    }
}