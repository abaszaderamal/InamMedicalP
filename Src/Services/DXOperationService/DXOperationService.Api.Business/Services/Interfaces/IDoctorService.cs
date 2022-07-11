using Med.Shared.Dtos;
using Med.Shared.Dtos.Doctor;
using Med.Shared.Entities;

namespace DXOperationService.Api.Business.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<Response<List<Doctor>>> GetAllAsync();
        Task<Response<Doctor>> GetByIdAsync(int id);
        Task<Response<List<DoctorDto2>>> GetAllByIdsAsync(string Ids, string userId);

    }
}
