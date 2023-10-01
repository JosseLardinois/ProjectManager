namespace ProjectManager.Interfaces
{
    public interface IArtefactService
    {
        Task CreateArtefacts(Guid phaseId);
    }
}
