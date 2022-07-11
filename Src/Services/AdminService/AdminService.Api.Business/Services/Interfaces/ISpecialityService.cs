using Med.Shared.Dtos;
using Med.Shared.Dtos.Clinic;
using Med.Shared.Dtos.Speciality;
using Med.Shared.Entities;

namespace AdminService.Api.Business.Services.Interfaces
{
    public interface ISpecialityService
    {
        Task<Response<NoContent>> CreateAsync(SpecialityPostDto specialityPostDto);
        Task<Response<NoContent>> UpdateAsync(SpecialityUpdateDto specialityUpdateDto);
        Task<Response<NoContent>> DeleteAsync(int id);
        Task<Response<List<Speciality>>> GetAllAsync();
        Task<Response<Speciality>> GetByIdAsync(int id);
    }
}
