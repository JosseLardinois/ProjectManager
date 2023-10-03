using ProjectManager.DTO;
using ProjectManager.Models;

namespace ProjectManager.Interfaces
{
    public interface IArtefactService
    {
        Task<IEnumerable<ArtefactDTO>> GetArtefactsFromPhase(Guid phaseId);
        Task<bool> CreateArtefacts(List<PhaseDTO> phases);
    }
}
