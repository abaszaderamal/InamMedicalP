namespace DXOperationService.Api.Business.Services.Interfaces
{
    public interface IServiceUnitOfWork
    {
        public IDXOperationService DxOperationService { get; }
        public IDoctorService DoctorService { get; }
        public IMedicineService MedicineService { get; }
    }
}
