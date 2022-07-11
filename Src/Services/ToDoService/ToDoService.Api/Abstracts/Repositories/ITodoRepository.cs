using Med.Shared.Dtos.Todo;
using Med.Shared.Entities;

namespace ToDoService.Api.Abstracts.Repositories
{
    public interface ITodoRepository : IRepository<Todo>
    {
        List<ToDoDto> GetAll(string userId);
    }
}
