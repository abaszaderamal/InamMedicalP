using AdminService.Api.Core.Abstracts.Repositories;
using AdminService.Api.Data.DAL;
using Med.Shared.Entities;

namespace AdminService.Api.Data.Concrete.Implementations
{
    public class MedCategoryRepository : Repository<MedCategory>,IMedCategoryRepository
    {
        private AppDbContext _context;
        public MedCategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
