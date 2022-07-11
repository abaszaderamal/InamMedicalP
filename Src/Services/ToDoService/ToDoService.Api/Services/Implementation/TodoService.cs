using System.Diagnostics.CodeAnalysis;
using Med.Shared.Dtos;
using Med.Shared.Dtos.Todo;
using Med.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using ToDoService.Api.Abstracts;
using ToDoService.Api.Services.Interface;

namespace ToDoService.Api.Services.Implementation
{
    public class TodoService : ITodoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TodoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<List<Todo>>> GetAllAsync()
        {
            var result =await _unitOfWork.TodoRepository.GetAllAsync(p => p.IsDeleted == false);
            return Response<List<Todo>>.Success(result ,StatusCodes.Status200OK);
        }

        public async Task<Response<Todo>> GetByIdAsync(int id)
        {
            var result = await _unitOfWork.TodoRepository.GetAsync(p=> p.IsDeleted==false && p.Id == id );
            if (result is null)
            {
                return Response<Todo>.Fail("Todo not found.", StatusCodes.Status404NotFound);
            }

            return Response<Todo>.Success(result, StatusCodes.Status200OK);
        }

        public async Task<Response<NoContent>> CreateAsync(TodoPostDto todoPostDto,string userId)
        {
            Todo todo = new Todo()
            {
                Name = todoPostDto.Name,
                Note = todoPostDto.Note,
                CreatedAt = DateTime.UtcNow,
                Status = todoPostDto.Status,
                AppUserId = userId,
            };

            await _unitOfWork.TodoRepository.CreateAsync(todo);
            await _unitOfWork.Save();
            return Response<NoContent>.Success(StatusCodes.Status200OK);
        }

        
        public async Task<Response<NoContent>> UpdateAsync(TodoUpdateDto todoUpdateDto)
        {
            
            Todo todoDb = await _unitOfWork.TodoRepository.GetAsync(p => p.Id == todoUpdateDto.Id && p.IsDeleted == false );
            if (todoDb is null)
            {
                return Response<NoContent>.Fail("Todo not found.", StatusCodes.Status404NotFound);
            }

            todoDb.Name = todoUpdateDto.Name;
            todoDb.Note = todoUpdateDto.Note;
            todoDb.CreatedAt = DateTime.UtcNow;
            todoDb.Status = todoUpdateDto.Status;
            
            
            _unitOfWork.TodoRepository.Update(todoDb);
            await _unitOfWork.Save();
            return Response<NoContent>.Success(StatusCodes.Status200OK);


        }

        
        public async Task<Response<NoContent>> DeleteAsync(int id)
        {
            Todo todoDb = await _unitOfWork.TodoRepository.GetAsync(p => p.Id == id);
            if (todoDb == null)
            {
                return Response<NoContent>.Fail("Task is not found", StatusCodes.Status404NotFound);
            }

            todoDb.IsDeleted = true;
            await _unitOfWork.Save();
            return Response<NoContent>.Success(StatusCodes.Status200OK);
        }

        
    }
}
