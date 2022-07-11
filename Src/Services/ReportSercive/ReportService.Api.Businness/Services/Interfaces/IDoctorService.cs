using Med.Shared.Dtos;
using Med.Shared.Dtos.Doctor;
using Med.Shared.Entities;

namespace ReportService.Api.Businness.Services.Interfaces
{
    public interface IDoctorService
    {
        Task Create(DoctorClinicPostDto doctorPostDto);
        Task Update(int id, DoctorUpdateDto doctorUpdateDto);
        Task DeleteAsync(int id);
        Task<Response<List<DoctorDto>>> GetAllAsync();

        Task<Response<DoctorDto>> GetByIdAsync(int id);
    }
}
