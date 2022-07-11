using AdminService.Api.Business.Services.Interfaces;
using AdminService.Api.Core.Abstracts;
using AdminService.Api.Core.Abstracts.Repositories;

namespace AdminService.Api.Business.Services.Implementations
{
    public class ServiceUnitOfWork : IServiceUnitOfWork
    {
        
        private IClinicService _clinicService;
        private ISpecialityService _specialityService;
        private ITagService _tagService;
        private IMedicineService _medicineService;
        private IDoctorService _doctorService;
        private IMedCategoryService _medCategoryService;
        private readonly IUnitOfWork _unitOfWork;
        public ServiceUnitOfWork(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IClinicService ClinicService => _clinicService ??= new ClinicService(_unitOfWork);
        public ISpecialityService SpecialityService  => _specialityService ??= new SpecialityService(_unitOfWork);
        public ITagService TagService => _tagService ??= new TagService(_unitOfWork);
        public IMedicineService MedicineService  => _medicineService ??= new MedicineService(_unitOfWork);
        public IDoctorService DoctorService => _doctorService ??= new DoctorService(_unitOfWork);
        public IMedCategoryService MedCategoryService => _medCategoryService ??= new MedCategoryService(_unitOfWork);
    }
}
