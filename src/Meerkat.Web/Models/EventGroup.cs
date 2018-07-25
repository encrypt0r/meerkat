using System.Collections.Generic;

namespace Meerkat.Web.Models
{
    public class EventGroup
    {
        public long Id { get; set; }
      
        public string Fingerprint { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public long? FirstSeenId { get; set; }
        public Event FirstSeen { get; set; }

        public long? LastSeenId { get; set; }
        public Event LastSeen { get; set; }
    }
}
