using Med.Shared.Dtos;
using Med.Shared.Dtos.Evaluation;
using Med.Shared.Dtos.User;
using Med.Shared.Entities;

namespace EvaluationService.Api.Abstracts.Repositories
{
    public interface IEvaluationRepository:IRepository<Evaluation>
    {
        Task<Response<List<EvaluationGetDto>>> GetAllUserAsync(string userId);

        Task<int> GetUserEvaluationByIdAsync(string userId);
        Task<bool> CheckRaitingStatus(string UserId);
        Task  SetAVGValue(string Owner);


        //Task<int> GetUserEvaluationValueByIdAsync(string userId);
    }
}
