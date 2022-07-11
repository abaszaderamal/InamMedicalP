using System.IO.Compression;
using AdminService.Api.Core.Abstracts;
using AdminService.Api.Core.Abstracts.Repositories;
using AdminService.Api.Data.Concrete.Implementations;
using AdminService.Api.Data.DAL;

namespace AdminService.Api.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IClinicRepository _clinicRepository;
        private ISpecialityRepository _specialityRepository;
        private ITagRepository _tagRepository;
        private IMedRepository _medRepository;
        private IMedCategoryRepository _medCategoryRepository;
        private IDoctorRepository _doctorRepository;
        private IClinicDoctorRepository _clinicDoctorRepository;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IClinicRepository clinicRepository => _clinicRepository ??= new ClinicRepository(_context);
        public ISpecialityRepository specialityRepository => _specialityRepository ??= new SpecialityRepository(_context);
        public ITagRepository tagRepository => _tagRepository ??= new TagRepository(_context);
        public IMedRepository MedRepository => _medRepository ??= new MedRepository(_context);
        public IMedCategoryRepository MedCategoryRepository => _medCategoryRepository ??= new MedCategoryRepository(_context);
        public IDoctorRepository DoctorRepository  => _doctorRepository ??= new DoctorRepository(_context);
        public IClinicDoctorRepository ClinicDoctorRepository => _clinicDoctorRepository ??= new ClinicDoctorRepository(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
