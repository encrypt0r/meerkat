using Meerkat.Dtos;
using Meerkat.Web.Data;
using Meerkat.Web.Helpers;
using Meerkat.Web.Models;
using Meerkat.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meerkat.Web.Areas.Api
{
    [Area("Api")]
    [Route("[area]/[controller]")]
    public class EventsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventStatisticsService _statisticsService;

        public EventsController(IUnitOfWork unitOfWork, IEventStatisticsService statisticsService)
        {
            _unitOfWork = unitOfWork;
            _statisticsService = statisticsService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateEventDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.FirstOrDefault());

            var model = new Event
            {
                Message = dto.Message,
                Level = dto.Level,
                Date = dto.Date ?? DateTime.UtcNow,
                Release = dto.Release,
                RootCause = dto.RootCause,
                Type = dto.Type,
                Username = dto.Username,
                Runtime = dto.Runtime,
                OperatingSystem = dto.OperatingSystem,
                MachineName = dto.MachineName,
                Sdk = dto.Sdk,
                SdkVersion = dto.SdkVersion,
                OSArchitecture = dto.OSArchitecture,
                Module = dto.Module,
                ModuleVersion = dto.ModuleVersion,
            };

            if (dto.StackTrace != null)
            {
                model.StackTrace = dto.StackTrace.Select(fdto =>
                {
                    return new Frame
                    {
                        Event = model,
                        ColumnNumber = fdto.ColumnNumber,
                        FileName = fdto.FileName,
                        ContextLine = fdto.ContextLine,
                        Function = fdto.Function,
                        InApp = fdto.InApp,
                        LineNumber = fdto.LineNumber,
                        Module = fdto.Module,
                    };
                }).ToList();
            }

            // Fingerprint must be the last property set, because it depends on the value of the other properties
            model.Fingerprint = EventHelper.GetFingerprint(model);

            var group = await _unitOfWork.EventGroups.GetByFingerprint(model.Fingerprint);
            if (group == null)
            {
                group = new EventGroup
                {
                    FirstSeen = model,
                    Fingerprint = model.Fingerprint,
                };

                _unitOfWork.EventGroups.Add(group);
                await _unitOfWork.CompleteAsync();
            }

            if (group.Events == null)
                group.Events = new List<Event>();

            group.LastSeen = model;
            group.Events.Add(model);

            await _unitOfWork.CompleteAsync();

            _statisticsService.DataModified();

            return Created(string.Empty, null);
        }
    }
}
