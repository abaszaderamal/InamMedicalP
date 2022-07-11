using AdminService.Api.Business.Services.Interfaces;
using AdminService.Api.Data.DAL;
using Med.Shared.Dtos;
using Med.Shared.Dtos.Speciality;
using Med.Shared.Entities;
using Med.Shared.HttpStatusCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialityController : ControllerBase
    {
        private IServiceUnitOfWork _serviceUnitOfWork;
        private AppDbContext _context;

        public SpecialityController(AppDbContext context, IServiceUnitOfWork serviceUnitOfWork)
        {
            _context = context;
            _serviceUnitOfWork = serviceUnitOfWork;

        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]

        public async Task<Response<List<Speciality>>> GetAllAsync()
        {

            return await _serviceUnitOfWork.SpecialityService.GetAllAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]

        public async Task<Response<Speciality>> GetByIdAsync(int id)
        {
            return await _serviceUnitOfWork.SpecialityService.GetByIdAsync(id);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]

        public async Task<Response<NoContent>> CreateSpecialityAsync([FromBody] SpecialityPostDto specialityPostDto)
        {
            if (!ModelState.IsValid) return Response<NoContent>.Fail("Validation Problem", CStatusCodes.Status1017ValidationProblem);

            return await _serviceUnitOfWork.SpecialityService.CreateAsync(specialityPostDto);
        }

        [HttpPut]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]

        public async Task<Response<NoContent>> Update([FromBody] SpecialityUpdateDto specialityUpdateDto)
        {
            if (!ModelState.IsValid) return Response<NoContent>.Fail("Validation Problem", CStatusCodes.Status1017ValidationProblem);

            return await _serviceUnitOfWork. SpecialityService.UpdateAsync(specialityUpdateDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]

        public async Task<Response<NoContent>> Delete(int id)
        {
            return await _serviceUnitOfWork.SpecialityService.DeleteAsync(id);
        }


    }
}
