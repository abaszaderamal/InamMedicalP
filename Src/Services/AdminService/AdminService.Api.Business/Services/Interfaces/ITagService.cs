using Med.Shared.Dtos;
using Med.Shared.Dtos.Speciality;
using Med.Shared.Dtos.Tag;
using Med.Shared.Entities;

namespace AdminService.Api.Business.Services.Interfaces
{
    public interface ITagService
    {
        Task<Response<NoContent>> CreateAsync(TagPostDto tagPostDto);
        Task<Response<NoContent>> UpdateAsync(TagUpdateDto tagUpdateDto );
        Task<Response<NoContent>> DeleteAsync(int id);
        Task<Response<List<Tag>>> GetAllAsync();
        Task<Response<Tag>> GetByIdAsync(int id);
    }
}
