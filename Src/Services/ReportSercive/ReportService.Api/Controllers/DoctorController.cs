using Med.Shared.ControllerBases;
using Med.Shared.Dtos;
using Med.Shared.Dtos.Doctor;
using Med.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReportService.Api.Businness.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReportService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : CMControllerBase
    {
        private readonly IServiceUnitOfWork _serviceUnitOfWork;

        public DoctorController(IServiceUnitOfWork serviceUnitOfWork)
        {
            _serviceUnitOfWork = serviceUnitOfWork;

        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]
        public async Task<Response<List<DoctorDto>>> GetAll()
        {
            return await _serviceUnitOfWork.DoctorService.GetAllAsync();
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]
        public async Task<Response<DoctorDto>> GetById(int id)
        {

            return await _serviceUnitOfWork.DoctorService.GetByIdAsync(id);
        }

    }
}
