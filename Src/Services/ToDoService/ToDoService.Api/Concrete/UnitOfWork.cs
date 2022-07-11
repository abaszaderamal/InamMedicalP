using ToDoService.Api.Abstracts;
using ToDoService.Api.Abstracts.Repositories;
using ToDoService.Api.Concrete.Implementation;
using ToDoService.Api.DAL;

namespace ToDoService.Api.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _context { get; set; }

        private ITodoRepository _TodoRepository;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }


        public ITodoRepository TodoRepository => _TodoRepository ??= new TodoRepository(_context);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
