using ProjectManager.Models;
using System.Threading.Tasks;

namespace ProjectManager.DAL
{
    public interface IProjectRepository
    {
        Task<int> CreateProjectAsync(Project project);
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(int projectId);
    }
}
