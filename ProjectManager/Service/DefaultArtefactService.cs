using ProjectManager.DAL;
using ProjectManager.DTO;
using ProjectManager.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Service
{
    public class DefaultArtefactService : IDefaultArtefactService
    {
        private readonly IDefaultArtefactRepository _defaultArtefactRepository;
        public DefaultArtefactService(IDefaultArtefactRepository defaultArtefactRepository)
        {
            _defaultArtefactRepository = defaultArtefactRepository;
        }

        public async Task<List<DefaultArtefactDTO>> GetAllDefaultArtefacts()
        {
            var defaultArtefacts = await _defaultArtefactRepository.GetAllArtefacts();
            return defaultArtefacts.Select(MapToDTO).ToList();
        }



        private DefaultArtefact MapToModel(DefaultArtefactDTO defaultArtefactDto)
        {
            return new DefaultArtefact
            {
                Id = defaultArtefactDto.Id,
                Name = defaultArtefactDto.Name,
                Artefact_Type = defaultArtefactDto.Artefact_Type,
                Phase = defaultArtefactDto.Phase

            };
        }

        private DefaultArtefactDTO MapToDTO(DefaultArtefact defaultArtefact)
        {
            return new DefaultArtefactDTO
            {
                Id = defaultArtefact.Id,
                Name = defaultArtefact.Name,
                Artefact_Type = defaultArtefact.Artefact_Type,
                Phase = defaultArtefact.Phase
            };
        }
    }
}
