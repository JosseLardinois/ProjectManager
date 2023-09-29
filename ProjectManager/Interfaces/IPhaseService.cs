using ProjectManager.DTO;

namespace ProjectManager.Interfaces
{
    public interface IPhaseService
    {
        Task<bool> CreatePhases(Guid projectId);
        Task<IEnumerable<PhaseDTO>> GetAllPhasesAsync(Guid projectId);
    }
}
