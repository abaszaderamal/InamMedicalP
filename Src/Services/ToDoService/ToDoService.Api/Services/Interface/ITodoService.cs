using Med.Shared.Dtos;
using Med.Shared.Dtos.Todo;
using Med.Shared.Entities;

namespace ToDoService.Api.Services.Interface
{
    public interface ITodoService
    {
        Task<Response<NoContent>> CreateAsync(TodoPostDto todoPostDto, string userId);
        Task<Response<NoContent>> UpdateAsync(TodoUpdateDto todoUpdateDto);
        Task<Response<NoContent>> DeleteAsync(int id);
        Task<Response<List<Todo>>> GetAllAsync();
        Task<Response<Todo>> GetByIdAsync(int id);
    }
}
