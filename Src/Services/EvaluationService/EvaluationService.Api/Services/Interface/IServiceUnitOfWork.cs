namespace EvaluationService.Api.Services.Interface
{
    public interface IServiceUnitOfWork
    {
        public IRaitingService RaitingService { get; }
        public IEvaluationService  EvaluationService { get; }
    }
}
