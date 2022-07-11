using Med.Shared.Dtos;
using Med.Shared.Dtos.DXOperation;
using Med.Shared.Entities;

namespace DXOperationService.Api.Business.Services.Interfaces
{
    public interface IDXOperationService
    {
        Task<Response<NoContent>> CreateAsync(DXOperationPostDto dxOperationPostDto);
        Task<Response<NoContent>> UpdateAsync(DXOperationPutDto dxOperationPutDto);
        Task<Response<List<DXOperation>>> GetAllWithDocidsAsync(string Ids, string userId);
    }
}
