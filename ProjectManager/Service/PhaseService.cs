using ProjectManager.DTO;
using ProjectManager.DTOs;
using ProjectManager.Interfaces;
using ProjectManager.Models;

namespace ProjectManager.Service
{
    public class PhaseService : IPhaseService
    {
        private readonly IPhaseRepository _phaseRepository;
        public PhaseService(IPhaseRepository phaseRepository)
        {
            _phaseRepository = phaseRepository;
        }

        public async Task<bool> CreatePhases(Guid projectId)
        {
            return await _phaseRepository.CreatePhasesAsync(projectId);

            
        }
        public async Task<List<PhaseDTO>> GetAllPhasesAsync(Guid projectId)
        {
            var phases = await _phaseRepository.GetAllPhasesAsync(projectId);
            return phases.Select(MapToDTO).ToList(); // Model to DTO for outgoing response
        }

        public async Task<bool> UpdatePhaseAsync(PhaseDTO phaseDto)
        {
            var phase = MapToModel(phaseDto); // DTO to Model for incoming request
            return await _phaseRepository.UpdatePhaseAsync(phase);
        }

        public async Task<bool> IsAPhaseActive(Guid projectId)
        {
            return await _phaseRepository.IsAPhaseActive(projectId);
        }


        private Phase MapToModel(PhaseDTO phaseDto)
        {
            return new Phase
            {
                Id = phaseDto.Id,
                Name = phaseDto.Name,
                Completed = phaseDto.Completed,
                Status = phaseDto.Status,
                Completed_By = phaseDto.Completed_By,
                Completed_At = phaseDto.Completed_At,
                ProjectId = phaseDto.ProjectId,

            };
        }

        private PhaseDTO MapToDTO(Phase phase)
        {
            return new PhaseDTO
            {
                Id = phase.Id,
                Name = phase.Name,
                Completed = phase.Completed,
                Status = phase.Status,
                Completed_By = phase.Completed_By,
                Completed_At = phase.Completed_At,
                ProjectId = phase.ProjectId,
            };
        }

    }
}
