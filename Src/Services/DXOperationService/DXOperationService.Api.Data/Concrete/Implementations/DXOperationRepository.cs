using DXOperationService.Api.Core.Abstracts.Repositores;
using DXOperationService.Api.Data.DAL;
using Med.Shared.Entities;
using Med.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DXOperationService.Api.Data.Concrete.Implementations
{
    public class DXOperationRepository : Repository<DXOperation>, IDXOperationRepository
    {
        private readonly AppDbContext _context;
        public DXOperationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// userin gonderilen doc idlerine  aid olan operationlari getirir
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<DXOperation>> GetAllWithDocidsAsync(string Ids, string userId)
        {
            var ids = Ids.SplitToIntList();

            List<DXOperation> result = new List<DXOperation>();
            foreach (var id in ids)
            {
                List<DXOperation> res =await _context
                    .DXOperations
                    .Where(p => p.AppUserId == userId && p.DoctorId == id && p.IsDeleted == false).Include(p=>p.DXOperationMedicines).ToListAsync();
                result.AddRange(res);
            }

            return result;

        }


    }
}
