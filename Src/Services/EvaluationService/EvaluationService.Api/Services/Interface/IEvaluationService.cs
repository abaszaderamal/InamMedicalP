using Med.Shared.Dtos;
using Med.Shared.Dtos.Evaluation;
using Med.Shared.Dtos.User;
using Med.Shared.Entities;

namespace EvaluationService.Api.Services.Interface
{
    public interface IEvaluationService
    {
        Task<Response<NoContent>> CreateAsync(List<EvaluationPostDto> PostDto, string userId);
        Task<Response<NoContent>> UpdateAsync(EvaluationPutDto updateDto);
        Task<Response<NoContent>> DeleteAsync(int id);
        Task<Response<List<Raiting>>> GetAllAsync();
        Task<Response<List<EvaluationGetDto>>> GetAllUserAsync(string userId);
        Task<Response<Raiting>> GetByIdAsync(int id);
    }
}
