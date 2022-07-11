using DXOperationService.Api.Business.Services.Interfaces;
using DXOperationService.Api.Core.Abstracts;
using DXOperationService.Api.Core.Abstracts.Repositores;

namespace DXOperationService.Api.Business.Services.Implementations
{
    public class ServiceUnitOfWork : IServiceUnitOfWork
    {
        private readonly IUnitOfWork _unitOfWork;

        private IDoctorService _doctorService;
        private IDXOperationService _dxOperationService;
        private IMedicineService _medicineService;

        public ServiceUnitOfWork(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IDXOperationService DxOperationService => _dxOperationService ??=new DXOperationService(_unitOfWork);
        public IDoctorService DoctorService => _doctorService ??=
                                               new DoctorService(_unitOfWork);

        public IMedicineService MedicineService => _medicineService??= new MedicineService(_unitOfWork);
    }
}
