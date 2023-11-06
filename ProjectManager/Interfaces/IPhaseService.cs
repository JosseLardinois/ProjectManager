using ProjectManager.DTO;
using ProjectManager.Models;

namespace ProjectManager.Interfaces
{
    public interface IPhaseService
    {
        Task<List<PhaseDTO>> GetAllPhasesAsync(Guid projectId);
        Task<bool> UpdatePhaseAsync(PhaseDTO phaseDto);
        Task<bool> IsAPhaseActive(Guid projectId);
    }
}
