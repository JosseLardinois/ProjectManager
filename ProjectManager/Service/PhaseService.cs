using ProjectManager.DTO;
using ProjectManager.Interfaces;

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
        public async Task<IEnumerable<PhaseDTO>> GetAllPhasesAsync(Guid projectId)
        {
            var phases = await _phaseRepository.GetAllPhasesAsync(projectId);
            return phases;

        }

    }
}
