using Med.Shared.ControllerBases;
using Med.Shared.Dtos;
using Med.Shared.Dtos.Doctor;
using Med.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportService.Api.Businness.Services.Interfaces;
using ReportService.Api.Data.DAL;

namespace ReportService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : CMControllerBase
    {
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        private readonly AppDbContext _context;


        public HomeController( IServiceUnitOfWork serviceUnitOfWork)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
           
        }


        [HttpGet("Ok")]
        public IActionResult Ok()
        {

            return Ok("Okeydir");
        }


        [HttpGet("doctors")]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]
        //[Authorize]
        public async Task<Response<List<DoctorDto>>> GetAll()
        {
            return await _serviceUnitOfWork.DoctorService.GetAllAsync();

            //var c = await _context.Doctors.Include(p => p.ClinicDoctors).
            //    ThenInclude(p => p.Clinic)
            //    .Include(p => p.DXOperations).ThenInclude(p => p.AppUser)
            //    .Include(p => p.DXOperations).ThenInclude(p => p.DXOperationMedicines)
            //     .Include(p => p.Speciality).ToListAsync();

            // var json = JsonSerializer.Serialize(c);
            //var c = await _context.Doctors.Include(p=>p.Tag).Include(p => p.Speciality)
            //    .Include(p => p.ClinicDoctors).Include(p=>p.ClinicDoctors).ThenInclude(p=>p.Clinic).ToListAsync();
            //var d = c;
            //return Response<List<Doctor>>.Success(c, 200);


        }


        [HttpGet("{id}")]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]
        public async Task<Response<DoctorDto>> GetById(int id)
        {

            return await _serviceUnitOfWork.DoctorService.GetByIdAsync(id);
        }

       
    }

    public class Product
    {
        public string Name { get; set; }
    }
}
