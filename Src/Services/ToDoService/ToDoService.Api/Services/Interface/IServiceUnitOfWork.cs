namespace ToDoService.Api.Services.Interface
{
    public interface IServiceUnitOfWork
    {
        public ITodoService TodoService{ get; }
    }
}
