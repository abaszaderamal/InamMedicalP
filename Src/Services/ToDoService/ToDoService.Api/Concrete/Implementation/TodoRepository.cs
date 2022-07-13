using Med.Shared.Dtos.Todo;
using Med.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using ToDoService.Api.Abstracts.Repositories;
using ToDoService.Api.DAL;

namespace ToDoService.Api.Concrete.Implementation
{
    public class TodoRepository : Repository<Todo>, ITodoRepository
    {
        private readonly AppDbContext _context;
        public TodoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<List<ToDoDto>> GetAll(string userId)
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
            List<List<ToDoDto>> tores = new List<List<ToDoDto>>();
            for (int i = 0; i < todos.Count; i++)
            {
                if (todos[i] is null) continue;
                var t = new List<ToDoDto>();
                t.Add(todos[i]);

                for (int j = i+1; j < todos.Count; j++)
                {
                    if (todos[j] is null) continue;

                    if (todos[j].Date.Date == todos[i].Date.Date)
                    {
                        t.Add(todos[j]);
                        todos[j] = null;
                    }
                }
                tores.Add(t);

                //var t = todos
                //    .Where(p => p.Date.Date == todos[i].Date.Date)
                //    .ToList();
                //tores.Add(t);
                //foreach (var k in t)
                //{
                //    todos.Remove(k);
                //}
            }
            //foreach (var todo in todos)
            //{
            //    var t = todos
            //        .Where(p => p.Date.Date == todo.Date.Date)
            //        .ToList();
            //    tores.Add(t);
            //    foreach (var k in t)
            //    {
            //        todos.Remove(k);
            //    }
            //}
            //foreach (var todo in todos)
            //{
            //    var innerLi = new List<ToDoDto>();
            //    innerLi.Add(todo);

            //    foreach (var inTodo in todos)
            //    {
            //        if (inTodo==todo)
            //            continue;

            //        if (todo.Date.Date == inTodo.Date.Date)
            //        {
            //            innerLi.Add(inTodo);
            //            todos.Remove(inTodo);
            //        }
            //    }
            //    tores.Add(innerLi);
            //}

            //var rrr = tores;
            return tores;
        }
    }
}
