using ProjectManager.DTOs;
using ProjectManager.Models;
using System.Threading.Tasks;

namespace ProjectManager.DAL
{
    public interface IProjectRepository
    {
        Task<ProjectDTO> GetProjectAsync(Guid projectId);
        Task<int> CreateProjectAsync(Project project);
        Task<bool> UpdateProjectAsync(Project project);
        Task<bool> DeleteProjectAsync(Guid projectId);
    }
}
