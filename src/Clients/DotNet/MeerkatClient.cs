using Meerkat.Dtos;
using Meerkat.Helpers;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Meerkat
{
    public class MeerkatClient : IDisposable
    {
        private readonly HttpClient _httpClient;

        public MeerkatClient(string meerkatUrl)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(meerkatUrl);
        }

        public async Task ReportAsync(string message, EventLevel level)
        {
            var dto = new CreateEventDto
            {
                Message = message,
                Level = level,
            };

            FillContext(dto);

            await ReportEventAsync(dto);
        }

        public async Task<bool> ReportAsync(Exception e)
        {
            // Report inner-most exception, since it has the most useful information
            while (e.InnerException != null)
            {
                if (e.InnerException?.TargetSite == null)
                    break;

                e = e.InnerException;
            }

            var culpritAssemblyInfo = e.TargetSite?.ReflectedType?.Assembly?.GetName()
                                      ?? Assembly.GetEntryAssembly().GetName();

            var dto = new CreateEventDto
            {
                Message = e.Message,
                Level = EventLevel.Error,
                Type = e.GetType().Name,
                RootCause = $"{e.TargetSite?.Name}() in {e.TargetSite?.ReflectedType?.FullName ?? "<DynamicType>"}",
                Module = culpritAssemblyInfo.Name,
                ModuleVersion = culpritAssemblyInfo.Version.ToString(),
            };

            var trace = new StackTrace(e, true);

            dto.StackTrace = trace.GetFrames().Select(f =>
            {
                return new CreateFrameDto
                {
                    Function = f.GetMethod().Name,
                    ColumnNumber = f.GetFileColumnNumber(),
                    LineNumber = f.GetFileLineNumber(),
                    FileName = f.GetFileName(),
                    Module = f.GetMethod().DeclaringType.FullName,
                    ContextLine = f.GetMethod().ToString(),
                    InApp = !IsFrameworkModule(f.GetMethod().DeclaringType.FullName),
                };

            }).ToList();

            FillContext(dto);

            return await ReportEventAsync(dto);
        }

        private async Task<bool> ReportEventAsync(CreateEventDto dto)
        {
            try
            {
                var appSecurityProtocol = ServicePointManager.SecurityProtocol;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("Api/Events", content);

                ServicePointManager.SecurityProtocol = appSecurityProtocol;
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                // TODO: Report sdk exceptions
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private void FillContext(CreateEventDto dto)
        {
            dto.Date = DateTime.UtcNow;
            dto.MachineName = Environment.MachineName;
            dto.Username = Environment.UserName;
            dto.OperatingSystem = EnvironmentHelper.GetOperatinSystemName();
            dto.Runtime = EnvironmentHelper.GetRuntimeVersion();
            dto.OSArchitecture = EnvironmentHelper.GetOSArchitecture();
            dto.Release = Assembly.GetEntryAssembly().GetName().Version.ToString();
            dto.Sdk = ClientDefaults.SdkName;
            dto.SdkVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private bool IsFrameworkModule(string module)
        {
            return (module != null) && (module.StartsWith("Microsoft.") || module.StartsWith("System."));
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
