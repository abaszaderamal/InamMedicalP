using EvaluationService.Api.Abstracts.Repositories;
using EvaluationService.Api.DAL;
using Med.Shared.Entities;

namespace EvaluationService.Api.Concretes.Implementation
{
    public class EvaluationRatingRepository: Repository<EvaluationRating>, IEvaluationRatingRepository
    {
        public EvaluationRatingRepository(AppDbContext context) : base(context)
        {
        }
    }
}
