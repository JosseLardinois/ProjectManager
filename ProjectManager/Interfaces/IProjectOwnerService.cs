using ProjectManager.DTO;

namespace ProjectManager.Interfaces
{
    public interface IProjectOwnerService
    {
        Task<IEnumerable<ProjectOwnerDTO>> GetProjectOwnersByProjectIdAsync(Guid projectId);
        Task<bool> AddProjectOwnerAsync(ProjectOwnerDTO projectOwnerDto);
        Task<bool> RemoveProjectOwnerAsync(ProjectOwnerDTO projectOwnerDto);


    }
}
