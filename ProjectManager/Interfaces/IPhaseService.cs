using ProjectManager.DTO;
using ProjectManager.Models;

namespace ProjectManager.Interfaces
{
    public interface IPhaseService
    {
        Task<bool> CreatePhases(Guid projectId);
        Task<List<PhaseDTO>> GetAllPhasesAsync(Guid projectId);
    }
}
