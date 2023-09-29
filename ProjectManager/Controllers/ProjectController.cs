using Microsoft.AspNetCore.Mvc;
using ProjectManager.Services;
using System.Threading.Tasks;

namespace ProjectManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _projectService.GetProjectAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [HttpGet("{id}/phases")]
        public async Task<IActionResult> GetPhasesOfProject(int id)
        {
            return Ok();
        }

        [HttpGet("{id}/tasks")]
        public async Task<IActionResult> GetTasksOfProject(int id)
        {

            return Ok();
        }
    }
}
