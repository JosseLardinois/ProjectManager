using ProjectManager.DTO;
using ProjectManager.Interfaces;

namespace ProjectManager.Service
{
    public class ArtefactService
    {
        private readonly IArtefactRepository _artefactRepository;
        public ArtefactService(IArtefactRepository artefactRepository) 
        { 
            _artefactRepository = artefactRepository;
        }

        public async Task<bool> CreateArtefacts(Guid phaseId)
        {
            return await _artefactRepository.CreateArtefactsAsync(phaseId);
        }
    }
}
