using ProjectManager.DAL;
using ProjectManager.DTO;
using ProjectManager.Interfaces;
using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManager.Service
{
    public class ProjectOwnerService : IProjectOwnerService
    {
        private readonly IProjectOwnerRepository _projectOwnerRepository;

        public ProjectOwnerService(IProjectOwnerRepository projectOwnerRepository)
        {
            _projectOwnerRepository = projectOwnerRepository;
        }

        public async Task<IEnumerable<ProjectOwnerDTO>> GetProjectOwnersByProjectIdAsync(Guid projectId)
        {
            var projectOwners = await _projectOwnerRepository.GetProjectOwnersByProjectIdAsync(projectId);
            return projectOwners.Select(MapToDTO);
        }

        public async Task<bool> AddProjectOwnerAsync(ProjectOwnerDTO projectOwnerDto)
        {
            var projectOwner = MapToModel(projectOwnerDto);
            return await _projectOwnerRepository.AddProjectOwnerAsync(projectOwner.ProjectId, projectOwner.OwnerId);
        }

        public async Task<bool> RemoveProjectOwnerAsync(ProjectOwnerDTO projectOwnerDto)
        {
            var projectOwner = MapToModel(projectOwnerDto);
            return await _projectOwnerRepository.RemoveProjectOwnerAsync(projectOwner.ProjectId, projectOwner.OwnerId);
        }

        private ProjectOwner MapToModel(ProjectOwnerDTO projectOwnerDto)
        {
            return new ProjectOwner
            {
                ProjectId = projectOwnerDto.ProjectId,
                OwnerId = projectOwnerDto.OwnerId
            };
        }

        private ProjectOwnerDTO MapToDTO(ProjectOwner projectOwner)
        {
            return new ProjectOwnerDTO
            {
                ProjectId = projectOwner.ProjectId,
                OwnerId = projectOwner.OwnerId
            };
        }
    }
}
