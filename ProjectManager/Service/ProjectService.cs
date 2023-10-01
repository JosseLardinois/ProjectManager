using ProjectManager.DAL;
using ProjectManager.DTOs;
using ProjectManager.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IPhaseService _phaseService;
        private readonly IArtefactRepository _artefactRepository;

        public ProjectService(IProjectRepository projectRepository, IPhaseService phaseService)
        {
            _projectRepository = projectRepository;
            _phaseService = phaseService;
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

            var createproject = await _projectRepository.CreateProjectAsync(project);
            await _phaseService.CreatePhases(project.Id);
            return createproject;
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
