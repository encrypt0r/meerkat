﻿using Meerkat.Core.Dtos;
using Meerkat.Web.Data;
using Meerkat.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Meerkat.Web.Areas.Api
{
    [Area("Api")]
    [Route("[area]/[controller]")]
    public class EventsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Post([FromBody]CreateEventDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.FirstOrDefault());

            var model = new Event
            {
                Name = dto.Name,
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
            };

            model.Modules = string.Join(",", dto.Modules.Select(m => $"{m.Name}:{m.Version}").ToArray());
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

            _unitOfWork.Events.Add(model);
            await _unitOfWork.CompleteAsync();

            return Created(string.Empty, null);
        }
    }
}