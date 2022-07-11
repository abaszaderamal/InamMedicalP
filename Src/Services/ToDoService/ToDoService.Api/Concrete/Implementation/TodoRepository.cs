using Med.Shared.Entities;
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
    }
}
