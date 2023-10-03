using ProjectManager.DTO;
using ProjectManager.Interfaces;

namespace ProjectManager.Service
{
    public class ArtefactService: IArtefactService
    {
        private readonly IArtefactRepository _artefactRepository;
        private readonly IPhaseService _phaseService;
        public ArtefactService(IArtefactRepository artefactRepository, IPhaseService phaseService) 
        { 
            _artefactRepository = artefactRepository;
            _phaseService = phaseService;
        }
        public async Task<IEnumerable<ArtefactDTO>> GetArtefactsFromPhase(Guid phaseId)
        {
            return await _artefactRepository.GetArtefactsFromPhase(phaseId);
            
        }

        public async Task<bool> CreateArtefacts(Guid phaseId)
        {
            return await _artefactRepository.CreateArtefactsAsync(phaseId);
        }
    }
}
