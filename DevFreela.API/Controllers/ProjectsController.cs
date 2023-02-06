﻿using DevFreela.Application.Commands.ProjectCommands;
using DevFreela.Application.InputModels;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data.SqlTypes;
using System.Security.Cryptography.Xml;

namespace DevFreela.API.Controllers
{
    [Route("api/Projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string query)
        {
            var projectsQuery = new GetAllProjectsQuery(query);
            var projects = await _mediator.Send(projectsQuery);
            return Ok(projects);
        }

        [HttpGet("{id}")]
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
            //return BadRequest
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new {id = id}, command); ;
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateProjectCommand command)
        {
            //return BadRequest

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
        public async Task<IActionResult> Finish(int id)
        {
            var command = new FinishProjectCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
