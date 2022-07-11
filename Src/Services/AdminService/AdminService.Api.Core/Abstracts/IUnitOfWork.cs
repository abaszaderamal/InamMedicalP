using AdminService.Api.Core.Abstracts.Repositories;

namespace AdminService.Api.Core.Abstracts
{
    public interface IUnitOfWork
    {
        public IClinicRepository clinicRepository { get; }
        public ISpecialityRepository specialityRepository { get; }
        public ITagRepository tagRepository { get; }
        public IMedRepository MedRepository { get; }
        public IMedCategoryRepository MedCategoryRepository{ get; }
        public IDoctorRepository DoctorRepository { get; }
        public IClinicDoctorRepository ClinicDoctorRepository { get; }
        Task SaveAsync();

    }
}
