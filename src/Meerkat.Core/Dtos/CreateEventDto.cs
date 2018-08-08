using System;
using System.Collections.Generic;

namespace Meerkat.Dtos
{
    public class CreateEventDto
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public DateTime? Date { get; set; }
        public string Username { get; set; }
        public EventLevel Level { get; set; }
        public string Release { get; set; }
        public string RootCause { get; set; }
        public string Type { get; set; }
        public ICollection<CreateFrameDto> StackTrace { get; set; }
        public string MachineName { get; set; }
        public string OperatingSystem { get; set; }
        public string Runtime { get; set; }
        public ICollection<ModuleDto> Modules { get; set; }
    }
}
