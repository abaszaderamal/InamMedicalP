using AdminService.Api.Core.Abstracts.Repositories;
using AdminService.Api.Data.DAL;
using Med.Shared.Entities;

namespace AdminService.Api.Data.Concrete.Implementations
{
    public class SpecialityRepository : Repository<Speciality>, ISpecialityRepository
    {
        private readonly AppDbContext _context;

        public SpecialityRepository(AppDbContext context) : base(context)
        {
            _context=context;
        }
    }
}
