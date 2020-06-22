using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatorExample.Domain.Entities.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediatorExample.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OfficeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(GetAllOfficesQuery query)
        {
            string response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
