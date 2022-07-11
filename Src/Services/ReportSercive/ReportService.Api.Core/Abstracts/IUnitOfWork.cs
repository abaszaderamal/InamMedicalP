using ReportService.Api.Core.Abstracts.Repositories;

namespace ReportService.Api.Core.Abstracts
{
    public interface IUnitOfWork
    {
        public IDoctorRepository doctorRepository{ get; }

        Task SaveAsync();
    }
}
