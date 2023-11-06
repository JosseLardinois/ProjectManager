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
        private readonly IDefaultArtefactService _defaultArtefactService;

        public ProjectController(IProjectService projectService, IPhaseService phaseService, IArtefactService artefact, IProjectOwnerService projectownerService, IDefaultArtefactService defaultArtefactService)
        {
            _projectService = projectService;
            _phaseService = phaseService;
            _artefactService = artefact;
            _projectownerService = projectownerService;
            _defaultArtefactService = defaultArtefactService;
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
        [HttpPost("CreateProject")]
        public async Task<IActionResult> CreateProject(ProjectDTO project, Guid ownerId)
        {
            try
            {
                await _projectService.CreateProjectAsync(project, ownerId);
                return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // Update method with error handling
        [HttpPut("UpdateProject")]
        public async Task<IActionResult> UpdateProject(ProjectDTO project)
        {
            try
            {
                var updatedProject = await _projectService.UpdateProjectAsync(project);
                if (updatedProject == false)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
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
                return StatusCode(500, ex);
            }
        }

        [HttpGet("/IsAPhaseActive")]
        public async Task<IActionResult> IsAPhaseActive(Guid projectId)
        {

            try
            {
                return Ok(await _phaseService.IsAPhaseActive(projectId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }


        [HttpPut("/UpdatePhase")]
        public async Task<IActionResult> UpdatePhase(PhaseDTO phase)
        {
            try
            {
                var updatedProject = await _phaseService.UpdatePhaseAsync(phase);
                if (updatedProject == false)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpGet("{id}/GetPhasesOfProject")]
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

        [HttpGet("{phaseid}/GetAllArtefactsFromPhase")]
        public async Task<IActionResult> GetAllArtefactsFromPhase(Guid phaseid)
        {
            try
            {
                var artefacts = await _artefactService.GetArtefactsFromPhase(phaseid);
                return Ok(artefacts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpGet("/GetAllArtefactsFromProject")]
        public async Task<IActionResult> GetAllArtefactsFromProject(Guid projectId)
        {
                var artefacts = await _artefactService.GetArtefactsFromProject(projectId);
                return Ok(artefacts);

        }

        [HttpGet("/GetStatusArtefactsFromPhase")]
        public async Task<IActionResult> GetStatusArtefactsFromPhase(Guid phaseId, string status)
        {
            try
            {
                var artefacts = await _artefactService.GetStatusArtefactsFromPhase(phaseId, status);
                return Ok(artefacts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpPut("/UpdateArtefactStatus")]
        public async Task<IActionResult> UpdateArtefactStatus(ArtefactDTO artefact)
        {
            try
            {
                return Ok(await _artefactService.UpdateArtefactStatusAsync(artefact));

            }
            catch (Exception ex)
            { 
                return StatusCode(500, ex);
            }
        }

        [HttpPost("CreateDefaultArtefact")]
        public async Task<IActionResult> CreateDefaultArtefact(DefaultArtefactDTO artefact)
        {
            if (artefact == null)
            {
                return BadRequest("Artefact data is null.");
            }

            var result = await _defaultArtefactService.CreateDefaultArtefactAsync(artefact);
            return Ok(result);
        }

        // PUT: api/DefaultArtefacts/{id}
        [HttpPut("{id}/UpdateDefaultArtefact")]
        public async Task<IActionResult> UpdateDefaultArtefact(DefaultArtefactDTO artefact)
        {
            var result = await _defaultArtefactService.UpdateDefaultArtefactAsync(artefact);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }

        // DELETE: api/DefaultArtefacts/{id}
        [HttpDelete("{id}/DeleteDefaultArtefact")]
        public async Task<IActionResult> DeleteDefaultArtefact(int id)
        {
            var result = await _defaultArtefactService.DeleteDefaultArtefactAsync(id);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }



        [HttpGet("/GetAllDefaultArtefacts")]
        public async Task<IActionResult> GetAllDefaultArtefacts()
        {
            try
            {
                return Ok(await _defaultArtefactService.GetAllDefaultArtefacts());
            }
            catch(Exception ex)
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
        public async Task<IActionResult> AddProjectOwners(ProjectOwnerDTO projectOwnerDTO)
        {
            return Ok(await _projectownerService.AddProjectOwnerAsync(projectOwnerDTO));
        }
    }
}
