using ProjectManager.DTO;

namespace ProjectManager.Interfaces
{
    public interface IArtefactService
    {
        Task<IEnumerable<ArtefactDTO>> GetArtefactsFromPhase(Guid phaseId);
        Task<bool> CreateArtefacts(Guid phaseId);
    }
}
