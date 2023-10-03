using ProjectManager.DAL;
using ProjectManager.DTO;
using ProjectManager.Interfaces;
using ProjectManager.Models;
using System.Collections.Generic;

namespace ProjectManager.Service
{
    public class ArtefactService : IArtefactService
    {
        private readonly IArtefactRepository _artefactRepository;
        public ArtefactService(IArtefactRepository artefactRepository) 
        { 
            _artefactRepository = artefactRepository;
        }
        public async Task<IEnumerable<ArtefactDTO>> GetArtefactsFromPhase(Guid phaseId)
        {
            var artefacts = await _artefactRepository.GetArtefactsFromPhase(phaseId);
            return artefacts.Select(MapToDTO);

        }

        public async Task<bool> CreateArtefacts(List<PhaseDTO> phaseDTOs)
        {
            return await _artefactRepository.CreateArtefactsAsync(phaseDTOs);
        }


        private Artefact MapToModel(ArtefactDTO artefactDto)
        {
            return new Artefact
            {
                Id = artefactDto.Id,
                Name = artefactDto.Name,
                Completed = artefactDto.Completed,
                Completed_By = artefactDto.Completed_By,
                Completed_At = artefactDto.Completed_At,
                Artefact_Type = artefactDto.Artefact_Type,

            };
        }

        private ArtefactDTO MapToDTO(Artefact artefact)
        {
            return new ArtefactDTO
            {
                Id = artefact.Id,
                Name = artefact.Name,
                Completed = artefact.Completed,
                Completed_By = artefact.Completed_By,
                Completed_At = artefact.Completed_At,
                Artefact_Type = artefact.Artefact_Type,
            };
        }
    }
}
