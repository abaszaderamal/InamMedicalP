using Med.Shared.Dtos.Todo;
using Med.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using ToDoService.Api.Abstracts.Repositories;
using ToDoService.Api.DAL;

namespace ToDoService.Api.Concrete.Implementation
{
    public class TodoRepository : Repository<Todo> , ITodoRepository
    {
        private readonly  AppDbContext _context;
        public TodoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<ToDoDto> GetAll(string userId)
        {
            var todos = _context.Todos
                .Where(p => p.IsDeleted == false && p.AppUserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new ToDoDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Note = p.Note,
                    Date = p.CreatedAt,
                    Status = p.Status
                }).AsNoTracking()
                .ToList();
            todos.Reverse();
            return todos;
        }
    }
}
