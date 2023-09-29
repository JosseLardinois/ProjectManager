using ProjectManager.DTO;
using ProjectManager.Interfaces;

namespace ProjectManager.Service
{
    public class ArtefactService
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

        }
    }
}
