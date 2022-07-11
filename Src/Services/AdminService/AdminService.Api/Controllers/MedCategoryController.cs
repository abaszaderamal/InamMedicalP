using AdminService.Api.Business.Services.Interfaces;
using AdminService.Api.Data.DAL;
using Med.Shared.ControllerBases;
using Med.Shared.Dtos;
using Med.Shared.Dtos.Doctor;
using Med.Shared.Dtos.MedCategory;
using Med.Shared.Entities;
using Med.Shared.HttpStatusCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedCategoryController : CMControllerBase
    {
        private IServiceUnitOfWork _serviceUnitOfWork;
        private AppDbContext _context;

        public MedCategoryController(AppDbContext context, IServiceUnitOfWork serviceUnitOfWork)
        {
            _context = context;
            _serviceUnitOfWork = serviceUnitOfWork;

        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]

        public async Task<Response<List<MedCategory>>> GetAllAsync()
        {

            return await _serviceUnitOfWork.MedCategoryService.GetAllAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]

        public async Task<Response<MedCategory>> GetByIdAsync(int id)
        {
            return await _serviceUnitOfWork.MedCategoryService.GetByIdAsync(id);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]

        public async Task<Response<NoContent>> CreateDoctorAsync([FromBody] MedCatPostDto medCatPostDto)
        {
            if (!ModelState.IsValid) return Response<NoContent>.Fail("Validation Problem", CStatusCodes.Status1017ValidationProblem);

            return await _serviceUnitOfWork.MedCategoryService.CreateAsync(medCatPostDto);
        }

        [HttpPut]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]

        public async Task<Response<NoContent>> Update([FromBody] MedCatUpdateDto medCatUpdateDto)
        {
            if (!ModelState.IsValid) return Response<NoContent>.Fail("Validation Problem", CStatusCodes.Status1017ValidationProblem);

            return await _serviceUnitOfWork.MedCategoryService.UpdateAsync(medCatUpdateDto);
        }

        [HttpDelete]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]

        public async Task<Response<NoContent>> Delete(int id)
        {
            return await _serviceUnitOfWork.MedCategoryService.DeleteAsync(id);
        }
    }
}
