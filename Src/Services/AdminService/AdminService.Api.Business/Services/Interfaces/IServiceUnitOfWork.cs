using AdminService.Api.Core.Abstracts.Repositories;

namespace AdminService.Api.Business.Services.Interfaces
{
    public interface IServiceUnitOfWork
    {
        public IClinicService ClinicService{ get; }
        public ISpecialityService SpecialityService{ get; }
        public ITagService TagService{ get; }
        public IMedicineService MedicineService{ get; }
        public IDoctorService DoctorService  { get;}
        public IMedCategoryService MedCategoryService{ get; }
    }
}
