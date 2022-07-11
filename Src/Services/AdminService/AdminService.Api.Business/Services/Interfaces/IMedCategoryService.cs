using Med.Shared.Dtos;
using Med.Shared.Dtos.Clinic;
using Med.Shared.Dtos.MedCategory;
using Med.Shared.Entities;

namespace AdminService.Api.Business.Services.Interfaces
{
    public interface IMedCategoryService
    {
        Task<Response<NoContent>> CreateAsync(MedCatPostDto medCatPostDto);
        Task<Response<NoContent>> UpdateAsync(MedCatUpdateDto medCatUpdateDto);
        Task<Response<NoContent>> DeleteAsync(int id);
        Task<Response<List<MedCategory>>> GetAllAsync();

        Task<Response<MedCategory>> GetByIdAsync(int id);
    }
}
