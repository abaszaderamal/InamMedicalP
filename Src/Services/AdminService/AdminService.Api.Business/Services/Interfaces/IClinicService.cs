using Med.Shared.Dtos;
using Med.Shared.Dtos.Clinic;
using Med.Shared.Dtos.Doctor;
using Med.Shared.Entities;

namespace AdminService.Api.Business.Services.Interfaces
{
    public interface IClinicService
    {
        Task<Response<NoContent>> CreateAsync(ClinicPostDto clinicPostDto);
        Task UpdateAsync(int id, ClinicUpdateDto clinicUpdateDto);
        Task DeleteAsync(int id);
        Task<Response<List<Clinic>>> GetAllAsync();

        Task<Response<Clinic>> GetByIdAsync(int id);
    }
}
