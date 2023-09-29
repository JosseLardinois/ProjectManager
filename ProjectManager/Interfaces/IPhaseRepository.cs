using ProjectManager.DTO;

namespace ProjectManager.Interfaces
{
    public interface IPhaseRepository
    {
        Task<bool> CreatePhasesAsync(Guid projectId);
        Task<IEnumerable<PhaseDTO>> GetAllPhasesAsync(Guid projectId);
    }
}
