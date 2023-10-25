using ProjectManager.Models;

namespace ProjectManager.Interfaces
{
    public interface IDefaultArtefactRepository
    {
        Task<List<DefaultArtefact>> GetAllArtefacts();

        Task<int> CreateDefaultArtefactAsync(DefaultArtefact artefact);

        Task<bool> UpdateDefaultArtefactAsync(DefaultArtefact artefact);

        Task<bool> DeleteDefaultArtefactAsync(int artefactId);
    }
}
