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

        public async Task<ProjectDTO> GetProjectAsync(Guid projectId)
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
                Id = projectDto.Id,
                Name = projectDto.Name,
                Finished = projectDto.Finished,
                Created_At = projectDto.Created_At,
            };

            return await _projectRepository.CreateProjectAsync(project);
        }

        public async Task<bool> UpdateProjectAsync(ProjectDTO projectDto)
        {
            if (projectDto == null)
                throw new ArgumentNullException(nameof(projectDto));

            // Convert DTO to Model
            var project = new Project
            {
                Id = projectDto.Id,
                Name = projectDto.Name,
                Finished = projectDto.Finished
            };

            return await _projectRepository.UpdateProjectAsync(project);
        }

        public async Task<bool> DeleteProjectAsync(Guid projectId)
        {
            if (projectId == Guid.Empty)
                throw new ArgumentException("Project ID must be greater than 0", nameof(projectId));

            return await _projectRepository.DeleteProjectAsync(projectId);
        }
    }
}
