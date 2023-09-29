using ProjectManager.DTOs;
using ProjectManager.Models;
using System.Threading.Tasks;

namespace ProjectManager.Services
{
    public interface IProjectService
    {
        Task<ProjectDTO> GetProjectAsync(int projectId);
        Task<int> CreateProjectAsync(ProjectDTO projectDto);
        Task<bool> UpdateProjectAsync(ProjectDTO projectDto);
        Task<bool> DeleteProjectAsync(int projectId); 
    }
}
