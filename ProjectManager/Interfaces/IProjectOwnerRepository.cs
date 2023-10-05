using ProjectManager.Models;

namespace ProjectManager.Interfaces
{
    public interface IProjectOwnerRepository
    {
        Task<IEnumerable<ProjectOwner>> GetProjectOwnersByProjectIdAsync(Guid projectId);
        Task<bool> AddProjectOwnerAsync(Guid projectId, Guid ownerId);
        Task<bool> RemoveProjectOwnerAsync(Guid projectId, Guid ownerId);


    }
}
