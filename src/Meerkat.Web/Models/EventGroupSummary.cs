using Meerkat.Web.Models;
using System;

namespace Meerkat.Web.Models
{
    public class EventGroupSummary
    {
        public EventGroupSummary(EventGroup model, int users, int hits)
        {
            Id = model.Id;
            Message = model.LastSeen.Message;
            Level = Enum.GetName(typeof(EventLevel), model.LastSeen.Level);
            Type = model.LastSeen.Type;
            LastSeen = model.LastSeen.Date;
            Hits = hits;
            RootCause = model.LastSeen.RootCause;
            FirstSeen = model.FirstSeen.Date;
            Users = users;
        }

        public long Id { get; set; }
        public string Message { get; }
        public string Level { get; }
        public string Type { get; }
        public DateTime FirstSeen { get; set; }
        public DateTime LastSeen { get; }
        public int Hits { get; }
        public int Users { get; set; }
        public string RootCause { get; }
    }
}
