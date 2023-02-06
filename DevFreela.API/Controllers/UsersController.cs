using DevFreela.Application.Commands.UserCommands;
using DevFreela.Application.InputModels;
using DevFreela.Application.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DevFreela.API.Controllers
{
    [Route("api/Users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userQuery = new GetUserQuery(id);
            var user = await _mediator.Send(userQuery);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }

        [HttpPut("{id}/Login")]
        public IActionResult Login(int id, [FromBody] object inputModel)
        {
            //TODO
            return NoContent();
        }
    }
}
