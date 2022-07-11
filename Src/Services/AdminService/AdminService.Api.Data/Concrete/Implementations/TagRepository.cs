using AdminService.Api.Core.Abstracts.Repositories;
using AdminService.Api.Data.DAL;
using Med.Shared.Entities;

namespace AdminService.Api.Data.Concrete.Implementations
{
    public class TagRepository : Repository<Tag>,ITagRepository
    {
        private AppDbContext _context;

        public TagRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
