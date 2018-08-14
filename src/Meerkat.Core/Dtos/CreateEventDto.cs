using System;
using System.Collections.Generic;

namespace Meerkat.Dtos
{
    public class CreateEventDto
    {
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
        public string OSArchitecture { get; set; }
        public string Module { get; set; }
        public string ModuleVersion { get; set; }
        public string Sdk { get; set; }
        public string SdkVersion { get; set; }
        public string Runtime { get; set; }
    }
}
