using DevFreela.Application.InputModels;
using DevFreela.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data.SqlTypes;
using System.Security.Cryptography.Xml;

namespace DevFreela.API.Controllers
{
    [Route("api/Projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        
        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public IActionResult Get(string query)
        {
            var projects = _projectService.GetAll(query);
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var project = _projectService.GetById(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewProjectInputModel inputModel)
        {
            //return BadRequest

            var id = _projectService.Create(inputModel);

            return CreatedAtAction(nameof(GetById), new {id = id}, inputModel); ;
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateProjectInputModel inputModel)
        {
            //return BadRequest

            _projectService.Update(inputModel);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //return Not found

            _projectService.Delete(id);

            return NoContent();
        }

        [HttpPost("{id}/Comments")]
        public IActionResult PostComment(int id, [FromBody] CreateCommentInputModel createCommentModel)
        {

            _projectService.CreateComment(createCommentModel);
            return NoContent();
        }

        [HttpPut("{id}/Start")]
        public IActionResult Start(int id)
        {
            _projectService.Start(id);
            return NoContent();
        }

        [HttpPut("{id}/Finish")]
        public IActionResult Finish(int id)
        {
            _projectService.Finish(id);
            return NoContent();
        }
    }
}
