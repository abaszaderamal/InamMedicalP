
using DXOperationService.Api.Core.Abstracts.Repositores;

namespace DXOperationService.Api.Core.Abstracts
{
    public interface IUnitOfWork
    {
         public IDXOperationRepository DXOperationRepository { get; }
         public IDoctorRepository DoctorRepository { get; }
         public IMedicineRepository MedicineRepository { get; }
         public IDXOperationMedicineRepository DxOperationMedicineRepository { get; }


        Task SaveAsync();
    }
}
