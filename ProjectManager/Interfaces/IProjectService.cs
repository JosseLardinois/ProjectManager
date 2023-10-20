using ProjectManager.DTO;
using ProjectManager.DTOs;
using ProjectManager.Models;
using System.Threading.Tasks;

namespace ProjectManager.Service
{
    public interface IProjectService
    {
        Task<ProjectDTO> GetProjectAsync(Guid projectId);
        Task<bool> CreateProjectAsync(ProjectDTO projectDto, Guid ownerId);
        Task<bool> UpdateProjectAsync(ProjectDTO projectDto);
        Task<bool> DeleteProjectAsync(Guid projectId); 
    }
}
