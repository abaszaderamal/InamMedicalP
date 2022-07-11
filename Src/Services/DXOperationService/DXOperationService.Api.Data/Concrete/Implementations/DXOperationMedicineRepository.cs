using DXOperationService.Api.Core.Abstracts.Repositores;
using DXOperationService.Api.Data.DAL;
using Med.Shared.Entities;

namespace DXOperationService.Api.Data.Concrete.Implementations
{
    public class DXOperationMedicineRepository : Repository<DXOperationMedicine>, IDXOperationMedicineRepository
    {
        public DXOperationMedicineRepository(AppDbContext context) : base(context)
        {
        }
    }
}
