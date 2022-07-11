using AdminService.Api.Core.Abstracts.Repositories;
using AdminService.Api.Data.DAL;
using Med.Shared.Entities;

namespace AdminService.Api.Data.Concrete.Implementations
{
    public class ClinicDoctorRepository : Repository<ClinicDoctor>, IClinicDoctorRepository
    {
        private AppDbContext _context;
        public ClinicDoctorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
