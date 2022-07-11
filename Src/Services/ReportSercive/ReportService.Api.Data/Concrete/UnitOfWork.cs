using ReportService.Api.Core.Abstracts;
using ReportService.Api.Core.Abstracts.Repositories;
using ReportService.Api.Data.Concrete.Implementations;
using ReportService.Api.Data.DAL;

namespace ReportService.Api.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IDoctorRepository _doctorRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IDoctorRepository doctorRepository => _doctorRepository ??= new DoctorRepository(_context);
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
