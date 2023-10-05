using Microsoft.AspNetCore.Mvc;
using ProjectManager.DTO;
using ProjectManager.DTOs;
using ProjectManager.Interfaces;
using ProjectManager.Models;
using ProjectManager.Service;
using System.Threading.Tasks;

namespace ProjectManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IPhaseService _phaseService;
        private readonly IArtefactService _artefactService;
        private readonly IProjectOwnerService _projectownerService;

        public ProjectController(IProjectService projectService, IPhaseService phaseService, IArtefactService artefact, IProjectOwnerService projectownerService)
        {
            _projectService = projectService;
            _phaseService = phaseService;
            _artefactService = artefact;
            _projectownerService = projectownerService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(Guid id)
        {
            var project = await _projectService.GetProjectAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // Create method with error handling
        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectDTO project, Guid ownerId)
        {
            try
            {
                await _projectService.CreateProjectAsync(project, ownerId);
                return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown)
                return StatusCode(500, ex);
            }
        }

        // Update method with error handling
        [HttpPut]
        public async Task<IActionResult> UpdateProject(ProjectDTO project)
        {
            try
            {
                var updatedProject = await _projectService.UpdateProjectAsync(project);
                if (updatedProject == false)
                {
                    return NotFound();
                }
                return Ok(updatedProject);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown)
                return StatusCode(500, ex);
            }
        }

        // Delete method with error handling
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            try
            {
                var project = await _projectService.DeleteProjectAsync(id);
                if (project == false)
                {
                    return NotFound();
                }
                return Ok(project);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown)
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}/phases")]
        public async Task<IActionResult> GetPhasesOfProject(Guid id)
        {
            try
            {
                return Ok(await _phaseService.GetAllPhasesAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}/projectowners")]
        public async Task<IActionResult> GetProjectOwners(Guid id)
        {
            return Ok(await _projectownerService.GetProjectOwnersByProjectIdAsync(id));
        }

        [HttpPost("/projectowners")]
        public async Task<IActionResult> PostProjectOwners(ProjectOwnerDTO projectOwnerDTO)
        {
            return Ok(await _projectownerService.AddProjectOwnerAsync(projectOwnerDTO));
        }
    }
}
