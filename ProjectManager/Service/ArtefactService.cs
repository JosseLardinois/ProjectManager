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

        public async Task<bool> CreateArtefacts(List<PhaseDTO> phaseDTOs, List<DefaultArtefactDTO> defaultArtefactDTOs)
        {
            return await _artefactRepository.CreateArtefactsAsync(phaseDTOs, defaultArtefactDTOs);
        }
        public async Task<bool> UpdateArtefactStatusAsync(ArtefactDTO artefactDto)
        {
            if (artefactDto == null)
            {
                throw new ArgumentNullException(nameof(artefactDto));
            }
            var artefact = MapToModel(artefactDto);
            return await _artefactRepository.UpdateArtefactStatus(artefact);
        }
        public async Task<IEnumerable<ProjectArtefactDTO>> GetArtefactsFromProject(Guid projectId)
        {
            return await _artefactRepository.GetArtefactsFromProject(projectId);
        }

        public async Task<IEnumerable<ArtefactDTO>> GetStatusArtefactsFromPhase(Guid phaseId, string status)
        {
            var artefacts = await _artefactRepository.GetStatusArtefactsFromPhase(phaseId, status);
            return artefacts.Select(MapToDTO);
        }

        private Artefact MapToModel(ArtefactDTO artefactDto)
        {
            return new Artefact
            {
                Id = artefactDto.Id,
                Status = artefactDto.Status,
                Completed_By = artefactDto.Completed_By,
                Completed_At = artefactDto.Completed_At,
                PhaseId = artefactDto.PhaseId,
                DefaultArtefactId = artefactDto.DefaultArtefactId
            };
        }

        private ArtefactDTO MapToDTO(Artefact artefact)
        {
            return new ArtefactDTO
            {
                Id = artefact.Id,
                Status = artefact.Status,
                Completed_By = artefact.Completed_By,
                Completed_At = artefact.Completed_At,
                PhaseId = artefact.PhaseId,
                DefaultArtefactId = artefact.DefaultArtefactId
            };
        }
    }
}
