using ProjectManager.DAL;
using ProjectManager.DTOs;
using ProjectManager.Models;
using System;
using System.Threading.Tasks;

namespace ProjectManager.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ProjectDTO> GetProjectAsync(int projectId)
        {
            return await _projectRepository.GetProjectAsync(projectId);
        }

        public async Task<int> CreateProjectAsync(ProjectDTO projectDto)
        {
            if (projectDto == null)
                throw new ArgumentNullException(nameof(projectDto));

            // Convert DTO to Model
            var project = new Project
            {
                ProjectID = projectDto.ProjectID,
                Name = projectDto.Name,
                Created_At = projectDto.Created_At,
                Owners = projectDto.Owners,
                PhaseID = projectDto.PhaseID
            };

            return await _projectRepository.CreateProjectAsync(project);
        }

        public async Task UpdateProjectAsync(ProjectDTO projectDto)
        {
            if (projectDto == null)
                throw new ArgumentNullException(nameof(projectDto));

            // Convert DTO to Model
            var project = new Project
            {
                ProjectID = projectDto.ProjectID,
                Name = projectDto.Name,
                Created_At = projectDto.Created_At,
                Owners = projectDto.Owners,
                PhaseID = projectDto.PhaseID
            };

            await _projectRepository.UpdateProjectAsync(project);
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            if (projectId <= 0)
                throw new ArgumentException("Project ID must be greater than 0", nameof(projectId));

            await _projectRepository.DeleteProjectAsync(projectId);
        }
    }
}
