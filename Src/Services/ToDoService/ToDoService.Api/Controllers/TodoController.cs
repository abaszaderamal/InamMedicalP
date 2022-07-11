using System.Security.Claims;
using Med.Shared.ControllerBases;
using Med.Shared.Dtos;
using Med.Shared.Dtos.Todo;
using Med.Shared.Entities;
using Med.Shared.HttpStatusCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoService.Api.DAL;
using ToDoService.Api.Services.Interface;

namespace ToDoService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class TodoController : CMControllerBase
    {


        private IServiceUnitOfWork _serviceUnitOfWork;
        private AppDbContext _context;

        public TodoController(AppDbContext context, IServiceUnitOfWork serviceUnitOfWork)
        {
            _context = context;
            _serviceUnitOfWork = serviceUnitOfWork;

        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]
        public async Task<Response<List<Todo>>> GetAll()
        {
            return await _serviceUnitOfWork.TodoService.GetAllAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]
        public async Task<Response<Todo>> GetById(int id)
        {
            return await _serviceUnitOfWork.TodoService.GetByIdAsync(id);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]
        public async Task<Response<NoContent>> Create([FromBody] TodoPostDto todoPostDto)
        {
            if (!ModelState.IsValid) return Response<NoContent>.Fail("Validation Problem", CStatusCodes.Status1017ValidationProblem);

            return await _serviceUnitOfWork.TodoService.CreateAsync(todoPostDto , HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [HttpPut]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]
        public async Task<Response<NoContent>> Update ([FromBody] TodoUpdateDto todoUpdateDto)
        {
            if (!ModelState.IsValid) return Response<NoContent>.Fail("Validation Problem", CStatusCodes.Status1017ValidationProblem);

            return await _serviceUnitOfWork.TodoService.UpdateAsync(todoUpdateDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]
        public async Task<Response<NoContent>> Delete(int id)
        {
            return await _serviceUnitOfWork.TodoService.DeleteAsync(id);
        }

    }
}
