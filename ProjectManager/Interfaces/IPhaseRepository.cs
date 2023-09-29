namespace ProjectManager.Interfaces
{
    public interface IPhaseRepository
    {
        Task CreatePhasesAsync(Guid projectId);
    }
}
