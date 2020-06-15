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
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Post(EmployeeCreateCommand command)
        {
            string response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(EmployeeUpdateCommand command)
        {
            string response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(EmployeeDeleteCommand command)
        {
            string result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
