using AdminService.Api.Core.Abstracts.Repositories;
using AdminService.Api.Data.DAL;
using Med.Shared.Entities;

namespace AdminService.Api.Data.Concrete.Implementations
{
    public class MedRepository : Repository<Medicine>,IMedRepository
    {
        private AppDbContext _context;
        public MedRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
