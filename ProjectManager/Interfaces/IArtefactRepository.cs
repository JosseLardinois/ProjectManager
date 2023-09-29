namespace ProjectManager.Interfaces
{
    public interface IArtefactRepository
    {
        Task<bool> CreateArtefactsAsync(Guid phaseId);
    }
}
