using AdminService.Api.Business.Services.Interfaces;
using AdminService.Api.Data.DAL;
using Med.Shared.ControllerBases;
using Med.Shared.Dtos;
using Med.Shared.Dtos.Doctor;
using Med.Shared.Entities;
using Med.Shared.HttpStatusCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : CMControllerBase
    {
        private IServiceUnitOfWork _serviceUnitOfWork;
        private AppDbContext _context;

        public DoctorController(AppDbContext context, IServiceUnitOfWork serviceUnitOfWork)
        {
            _context = context;
            _serviceUnitOfWork = serviceUnitOfWork;

        }

        [HttpGet]
        //[Authorize(Roles = "Member")]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]

        public async Task<Response<List<Doctor>>> GetAllAsync()
        {

            return await _serviceUnitOfWork.DoctorService.GetAllAsync();
        }
        [HttpGet("Ok")]
        public  IActionResult Ok()
        {

            return Ok("Okeydir");
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Member")]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]

        public async Task<Response<Doctor>> GetByIdAsync(int id)
        {
            return await _serviceUnitOfWork.DoctorService.GetByIdAsync(id);
        }

        [HttpPost]
        //[Authorize(Roles = "Member")]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]

        public async Task<Response<NoContent>> CreateDoctorAsync([FromBody] DoctorClinicPostDto postDoctor)
        {
            if (!ModelState.IsValid) return Response<NoContent>.Fail("Validation Problem", CStatusCodes.Status1017ValidationProblem);

            return await _serviceUnitOfWork.DoctorService.CreateAsync(postDoctor);
        }
    }
}
