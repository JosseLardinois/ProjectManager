﻿using Microsoft.AspNetCore.Mvc;
using ProjectManager.DTOs;
using ProjectManager.Models;
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

        // Create method with error handling
        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectDTO project)
        {
            try
            {
                await _projectService.CreateProjectAsync(project);
                return CreatedAtAction(nameof(GetProject), new { id = project.ProjectID }, project);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown)
                return StatusCode(500, "Internal server error");
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
                return StatusCode(500, "Internal server error");
            }
        }

        // Delete method with error handling
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
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
                return StatusCode(500, "Internal server error");
            }
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
