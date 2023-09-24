using ProjectManager.DTOs;
using System.Threading.Tasks;

namespace ProjectManager.Services
{
    public interface IProjectService
    {
        Task<int> CreateProjectAsync(ProjectDTO projectDto);
        Task UpdateProjectAsync(ProjectDTO projectDto);
        Task DeleteProjectAsync(int projectId);
    }
}
