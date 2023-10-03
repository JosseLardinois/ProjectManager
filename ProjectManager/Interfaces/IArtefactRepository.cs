using ProjectManager.DTO;

namespace ProjectManager.Interfaces
{
    public interface IArtefactRepository
    {
        Task<IEnumerable<ArtefactDTO>> GetArtefactsFromPhase(Guid phaseId);
        Task<bool> CreateArtefactsAsync(Guid phaseId);
    }
}
