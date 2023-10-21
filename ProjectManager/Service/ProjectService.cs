using ProjectManager.DAL;
using ProjectManager.DTO;
using ProjectManager.DTOs;
using ProjectManager.Interfaces;
using ProjectManager.Models;
using ZstdSharp.Unsafe;

namespace ProjectManager.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IPhaseService _phaseService;
        private readonly IArtefactService _artefactService;
        private readonly IProjectOwnerService _projectOwnerService;
        private readonly IDefaultArtefactService _defaultArtefactService;

        public ProjectService(IProjectRepository projectRepository, IPhaseService phaseService, IArtefactService artefactService, IProjectOwnerService projectOwnerService, IDefaultArtefactService defaultArtefactService)
        {
            _projectRepository = projectRepository;
            _phaseService = phaseService;
            _artefactService = artefactService;
            _projectOwnerService = projectOwnerService;
            _defaultArtefactService = defaultArtefactService;
        }

        public async Task<ProjectDTO> GetProjectAsync(Guid projectId)
        {
            var project = await _projectRepository.GetProjectAsync(projectId);
            return MapToDTO(project); // Model to DTO for outgoing response
        }

        public async Task<bool> CreateProjectAsync(ProjectDTO projectDto, Guid ownerId)
        {
            
            var projectId = Guid.NewGuid();
            projectDto.Id = projectId;
            var projectownerDto = new ProjectOwnerDTO();
            projectownerDto.ProjectId = projectId;
            projectownerDto.OwnerId = ownerId;


            var project = MapToModel(projectDto);
            //Create project
            await _projectRepository.CreateProjectAsync(project);
            //Create Phases
            await _phaseService.CreatePhases(project.Id);
            //Get Phases
            var phases = await _phaseService.GetAllPhasesAsync(project.Id);
            //Get Artefacts
            var defaultArtefacts = await _defaultArtefactService.GetAllDefaultArtefacts();
            //Create project artefacts
            await _artefactService.CreateArtefacts(phases, defaultArtefacts);
            //Add Owner To Project
            await _projectOwnerService.AddProjectOwnerAsync(projectownerDto);
            return true;
        }

        public async Task<bool> UpdateProjectAsync(ProjectDTO projectDto)
        {
            var project = MapToModel(projectDto);
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
