using ToDoService.Api.Abstracts.Repositories;

namespace ToDoService.Api.Abstracts
{
    public interface IUnitOfWork
    {
        public ITodoRepository TodoRepository{ get;}

        Task Save();
    }
}
