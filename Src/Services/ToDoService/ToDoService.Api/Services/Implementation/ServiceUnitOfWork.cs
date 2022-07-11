using ToDoService.Api.Abstracts;
using ToDoService.Api.Services.Interface;

namespace ToDoService.Api.Services.Implementation
{
    public class ServiceUnitOfWork : IServiceUnitOfWork
    {
        public ITodoService _todoService { get; set; }
        
        private readonly IUnitOfWork _unitOfWork;
        public ServiceUnitOfWork(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public ITodoService TodoService => _todoService ??= new TodoService(_unitOfWork);
    }
}
