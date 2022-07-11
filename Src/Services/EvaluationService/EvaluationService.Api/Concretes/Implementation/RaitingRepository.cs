using EvaluationService.Api.Abstracts.Repositories;
using EvaluationService.Api.DAL;
using Med.Shared.Entities;

namespace EvaluationService.Api.Concretes.Implementation
{
    public class RaitingRepository:Repository<Raiting>,IRaitingRepository
    {
        public RaitingRepository(AppDbContext context) : base(context)
        {
        }
    }
}
