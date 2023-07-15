using DevFreela.Application.Commands.ProjectCommands;
using DevFreela.Application.Queries.ProjectQueries;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data.SqlTypes;
using System.Security.Cryptography.Xml;
using Microsoft.AspNetCore.Authorization;

namespace DevFreela.API.Controllers
{
    [Route("api/Projects")]
    [Authorize(Roles = "client")]

    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> Get(GetAllProjectsQuery getAllProjectsQuery)
        {
            var projects = await _mediator.Send(getAllProjectsQuery);
            return Ok(projects);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> GetById(int id)
        {
            var projectQuery = new GetProjectByIdQuery(id);
            var project = await _mediator.Send(projectQuery);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand command)
        {
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new {id = id}, command); ;
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateProjectCommand command)
        {
            _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteProjectCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPost("{id}/Comments")]
        [Authorize(Roles = "client, freelancer")]
        public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/Start")]
        public async Task<IActionResult> Start(int id)
        {
            var command = new StartProjectCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}/Finish")]
        public async Task<IActionResult> Finish(int id, [FromBody] FinishProjectCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result)
                return BadRequest("O pagamento não pôde ser processado.");

            return Accepted();
        }
    }
}
