using ProjectManager.DTO;
using ProjectManager.Models;

namespace ProjectManager.Interfaces
{
    public interface IPhaseRepository
    {
        Task<bool> CreatePhasesAsync(Guid projectId);
        Task<IEnumerable<Phase>> GetAllPhasesAsync(Guid projectId);
    }
}
