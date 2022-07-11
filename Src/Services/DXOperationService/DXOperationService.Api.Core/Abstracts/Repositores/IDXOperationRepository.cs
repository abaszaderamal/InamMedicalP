using Med.Shared.Entities;

namespace DXOperationService.Api.Core.Abstracts.Repositores
{
    public interface IDXOperationRepository : IRepository<DXOperation>
    {
        Task<List<DXOperation>> GetAllWithDocidsAsync(string Ids, string userId);

    }

}
