using ProjectManager.DTO;
using ProjectManager.Models;

namespace ProjectManager.Interfaces
{
    public interface IDefaultArtefactService
    {
        Task<List<DefaultArtefactDTO>> GetAllDefaultArtefacts();

        Task<int> CreateDefaultArtefactAsync(DefaultArtefactDTO artefactDto);

        Task<bool> UpdateDefaultArtefactAsync(DefaultArtefactDTO artefactDto);
        Task<bool> DeleteDefaultArtefactAsync(int artefactId);
    }
}
