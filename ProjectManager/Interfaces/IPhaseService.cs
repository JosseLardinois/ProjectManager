namespace ProjectManager.Interfaces
{
    public interface IPhaseService
    {
        Task<bool> CreatePhases(Guid projectId);
    }
}
