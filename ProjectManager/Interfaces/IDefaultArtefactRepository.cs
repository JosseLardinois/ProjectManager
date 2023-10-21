using ProjectManager.Models;

namespace ProjectManager.Interfaces
{
    public interface IDefaultArtefactRepository
    {
        Task<List<DefaultArtefact>> GetAllArtefacts();
    }
}
