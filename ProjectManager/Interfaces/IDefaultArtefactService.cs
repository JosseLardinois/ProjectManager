using ProjectManager.DTO;

namespace ProjectManager.Interfaces
{
    public interface IDefaultArtefactService
    {
        Task<List<DefaultArtefactDTO>> GetAllDefaultArtefacts();
    }
}
