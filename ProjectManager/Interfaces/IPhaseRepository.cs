namespace ProjectManager.Interfaces
{
    public interface IPhaseRepository
    {
        Task<bool> CreatePhasesAsync(Guid projectId);
    }
}
