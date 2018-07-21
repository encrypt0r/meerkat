using System;
using System.Collections.ObjectModel;

namespace Meerkat.Web.Models
{
    public class Event
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string Username { get; set; }
        public EventLevel Level { get; set; }
        public string Release { get; set; }
        public string RootCause { get; set; }
        public string Type { get; set; }
        public virtual Collection<Frame> StackTrace { get; set; }
    }
}
