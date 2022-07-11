using AdminService.Api.Core.Abstracts.Repositories;
using AdminService.Api.Data.DAL;
using Med.Shared.Entities;

namespace AdminService.Api.Data.Concrete.Implementations
{
    public class DoctorRepository : Repository<Doctor>,IDoctorRepository
    {
        private AppDbContext _context;
        public DoctorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
