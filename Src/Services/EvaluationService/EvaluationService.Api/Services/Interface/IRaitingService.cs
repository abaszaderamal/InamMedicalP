using Med.Shared.Dtos;
using Med.Shared.Dtos.Raiting;
using Med.Shared.Entities;

namespace EvaluationService.Api.Services.Interface
{
    public interface IRaitingService
    {
        Task<Response<NoContent>> CreateAsync(RaitingPostDto  PostDto);
        Task<Response<NoContent>> UpdateAsync(RaitingUpdateDto updateDto);
        Task<Response<NoContent>> DeleteAsync(int id);
        Task<Response<List<RaitingDto>>> GetAllAsync();
        Task<Response<RaitingDto>> GetByIdAsync(int id);
    }
}
