using DXOperationService.Api.Business.Services.Interfaces;
using DXOperationService.Api.Core.Abstracts;
using DXOperationService.Api.Data.DAL;

namespace DXOperationService.Api.Business.Services.Implementations
{
    public class ServiceUnitOfWork : IServiceUnitOfWork
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        private IDoctorService _doctorService;
        private IDXOperationService _dxOperationService;
        private IMedicineService _medicineService;

        public ServiceUnitOfWork(IUnitOfWork unitOfWork,AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public IDXOperationService DxOperationService => _dxOperationService ??=new DXOperationService(_unitOfWork,_context);
        public IDoctorService DoctorService => _doctorService ??=
                                               new DoctorService(_unitOfWork);

        public IMedicineService MedicineService => _medicineService??= new MedicineService(_unitOfWork);
    }
}
