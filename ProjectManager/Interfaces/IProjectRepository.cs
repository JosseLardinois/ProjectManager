using ProjectManager.DTOs;
using ProjectManager.Models;
using System.Threading.Tasks;

namespace ProjectManager.DAL
{
    public interface IProjectRepository
    {
        Task<ProjectDTO> GetProjectAsync(int projectId);
        Task<int> CreateProjectAsync(Project project);
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(int projectId);
    }
}
