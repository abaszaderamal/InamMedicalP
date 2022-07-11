using EvaluationService.Api.Abstracts.Repositories;

namespace EvaluationService.Api.Abstracts
{
    public interface IUnitOfWork
    {

        public IRaitingRepository RaitingRepository { get; }
        public IEvaluationRepository EvaluationRepository { get; }
        public IEvaluationRatingRepository EvaluationRatingRepository { get; }

        Task SaveAsync();
    }
}
