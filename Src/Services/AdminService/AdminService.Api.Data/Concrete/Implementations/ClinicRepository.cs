using System.Linq.Expressions;
using AdminService.Api.Core.Abstracts.Repositories;
using AdminService.Api.Data.DAL;
using Med.Shared.Entities;

namespace AdminService.Api.Data.Concrete.Implementations
{
    public class ClinicRepository : Repository<Clinic>, IClinicRepository
    {
        private readonly AppDbContext _context;

        public ClinicRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

      
    }
}
