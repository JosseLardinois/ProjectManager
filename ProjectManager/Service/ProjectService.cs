using ProjectManager.DAL;
using ProjectManager.DTO;
using ProjectManager.DTOs;
using ProjectManager.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IPhaseService _phaseService;
        private readonly IArtefactService _artefactService;

        public ProjectService(IProjectRepository projectRepository, IPhaseService phaseService, IArtefactService artefactService)
        {
            _projectRepository = projectRepository;
            _phaseService = phaseService;
            _artefactService = artefactService;
        }

        public async Task<ProjectDTO> GetProjectAsync(Guid projectId)
        {
            var project = await _projectRepository.GetProjectAsync(projectId);
            return MapToDTO(project); // Model to DTO for outgoing response
        }

        public async Task<bool> CreateProjectAsync(ProjectDTO projectDto)
        {
            var project = MapToModel(projectDto); // DTO to Model for incoming request
            await _projectRepository.CreateProjectAsync(project);
            await _phaseService.CreatePhases(project.Id);
            var phases = await _phaseService.GetAllPhasesAsync(project.Id);
            await _artefactService.CreateArtefacts(phases);
            return true;
        }

        public async Task<bool> UpdateProjectAsync(ProjectDTO projectDto)
        {
            var project = MapToModel(projectDto); // DTO to Model for incoming request
            return await _projectRepository.UpdateProjectAsync(project);
        }

        public async Task<bool> DeleteProjectAsync(Guid projectId)
        {
            return await _projectRepository.DeleteProjectAsync(projectId);
        }

        private Project MapToModel(ProjectDTO projectDto)
        {
            return new Project
            {
                Id = projectDto.Id,
                Name = projectDto.Name,
                Finished = projectDto.Finished,
                Created_At = projectDto.Created_At
            };
        }

        private ProjectDTO MapToDTO(Project project)
        {
            return new ProjectDTO
            {
                Id = project.Id,
                Name = project.Name,
                Finished = project.Finished,
                Created_At = project.Created_At
            };
        }
    }
}
