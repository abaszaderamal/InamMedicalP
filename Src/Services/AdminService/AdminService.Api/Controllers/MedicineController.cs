using AdminService.Api.Business.Services.Interfaces;
using AdminService.Api.Data.DAL;
using Med.Shared.Dtos;
using Med.Shared.Dtos.Doctor;
using Med.Shared.Dtos.Medicine;
using Med.Shared.Entities;
using Med.Shared.HttpStatusCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private IServiceUnitOfWork _serviceUnitOfWork;
        private AppDbContext _context;

        public MedicineController(AppDbContext context, IServiceUnitOfWork serviceUnitOfWork)
        {
            _context = context;
            _serviceUnitOfWork = serviceUnitOfWork;

        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]

        public async Task<Response<List<Medicine>>> GetAll()
        {

            return await _serviceUnitOfWork.MedicineService.GetAllAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]

        public async Task<Response<Medicine>> GetById(int id)
        {
            return await _serviceUnitOfWork.MedicineService.GetByIdAsync(id);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]

        public async Task<Response<NoContent>> Create([FromBody] MedPostDto postMedicine)
        {
            if (!ModelState.IsValid) return Response<NoContent>.Fail("Validation Problem", CStatusCodes.Status1017ValidationProblem);

            return await _serviceUnitOfWork.MedicineService.CreateAsync(postMedicine);
        }

        [HttpPut]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]

        public async Task<Response<NoContent>> Update([FromBody] MedUpdateDto medUpdateDto)
        {
            if (!ModelState.IsValid) return Response<NoContent>.Fail("Validation Problem", CStatusCodes.Status1017ValidationProblem);

            return await _serviceUnitOfWork.MedicineService.UpdateAsync(medUpdateDto);
        }

        [HttpDelete]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]

        public async Task<Response<NoContent>> Delete(int id)
        {
            return await _serviceUnitOfWork.MedicineService.DeleteAsync(id);
        }
    }
}
