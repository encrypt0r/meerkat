﻿using Meerkat.Dtos;
using Meerkat.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
                e = e.InnerException;

            var dto = new CreateEventDto
            {
                Message = e.Message,
                Level = EventLevel.Error,
                RootCause = $"{e.TargetSite.Name} of {e.TargetSite.ReflectedType?.Name ?? "<DynamicType>"}",
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
                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("Api/Events", content);
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
            dto.Release = Assembly.GetEntryAssembly().GetName().Version.ToString();
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
