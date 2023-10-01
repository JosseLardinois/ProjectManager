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

        public async Task CreateArtefacts(Guid phaseId)
        {
            IEnumerable<PhaseDTO> phases = await _phaseService.GetAllPhasesAsync(phaseId);
            foreach (var phase in  phases)
            {
                Console.WriteLine(phase);
            }
        }
    }
}
