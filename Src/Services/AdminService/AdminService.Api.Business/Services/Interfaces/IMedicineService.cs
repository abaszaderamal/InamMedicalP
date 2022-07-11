using Med.Shared.Dtos;
using Med.Shared.Dtos.Clinic;
using Med.Shared.Dtos.Medicine;
using Med.Shared.Entities;

namespace AdminService.Api.Business.Services.Interfaces
{
    public interface IMedicineService
    {
        Task<Response<NoContent>> CreateAsync(MedPostDto medPostDto);
        Task<Response<NoContent>> UpdateAsync(MedUpdateDto medUpdateDto);
        Task<Response<NoContent>> DeleteAsync(int id);
        Task<Response<List<Medicine>>> GetAllAsync();

        Task<Response<Medicine>> GetByIdAsync(int id);
    }
}
