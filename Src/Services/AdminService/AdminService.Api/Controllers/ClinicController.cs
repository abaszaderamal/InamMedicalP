using AdminService.Api.Business.Services.Interfaces;
using AdminService.Api.Data.DAL;
using Med.Shared.ControllerBases;
using Med.Shared.Dtos;
using Med.Shared.Dtos.Clinic;
using Med.Shared.Entities;
using Med.Shared.HttpStatusCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicController : CMControllerBase
    {
  
        private IServiceUnitOfWork _serviceUnitOfWork;
        private AppDbContext _context;

        public ClinicController(AppDbContext context, IServiceUnitOfWork serviceUnitOfWork)
        {
            _context = context;
            _serviceUnitOfWork  = serviceUnitOfWork;
            
        }

        [HttpGet]
        //[Authorize(Roles = "Member")]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]


        public async Task<Response<List<Clinic>>> GetAllAsync()
        {
            
            return await _serviceUnitOfWork.ClinicService.GetAllAsync();
        }

        [HttpGet("{id}")]
        //[Authorize(Roles = "Member")]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]

        public async Task<Response<Clinic>> GetByIdAsync(int id)
        {
            return await _serviceUnitOfWork.ClinicService.GetByIdAsync(id);
        }

        [HttpPost]
        //[Authorize(Roles = "Member")]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]

        public async Task<Response<NoContent>> CreateClinicAsync([FromBody] ClinicPostDto clinicPostDto)
         {
             if (!ModelState.IsValid) return Response<NoContent>.Fail("Validation Problem",CStatusCodes.Status1017ValidationProblem);
            return await _serviceUnitOfWork.ClinicService.CreateAsync(clinicPostDto);
        }

    }
}
