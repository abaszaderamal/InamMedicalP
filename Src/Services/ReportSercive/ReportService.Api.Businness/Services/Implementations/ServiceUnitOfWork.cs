using ReportService.Api.Businness.Services.Interfaces;
using ReportService.Api.Core.Abstracts;

namespace ReportService.Api.Businness.Services.Implementations
{
    public class ServiceUnitOfWork : IServiceUnitOfWork
    {
            private readonly IUnitOfWork _unitOfWork;

            private IDoctorService _doctorService;

            public ServiceUnitOfWork(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public IDoctorService DoctorService => _doctorService ??= new DoctorService(_unitOfWork);
    }
}
