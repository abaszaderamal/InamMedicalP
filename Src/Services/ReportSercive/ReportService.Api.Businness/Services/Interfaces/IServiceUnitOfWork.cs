namespace ReportService.Api.Businness.Services.Interfaces
{
    public interface IServiceUnitOfWork
    {
        public IDoctorService DoctorService { get; }
    }

}
