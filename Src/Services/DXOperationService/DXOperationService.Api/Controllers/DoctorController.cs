using System.Security.Claims;
using DXOperationService.Api.Business.Services.Interfaces;
using Med.Shared.ControllerBases;
using Med.Shared.Dtos;
using Med.Shared.Dtos.Doctor;
using Med.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DXOperationService.Api.Controllers
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
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]
        public async Task<Response<List<DoctorDto2>>> Get([FromBody] string Ids)
        {
            return await _serviceUnitOfWork
                .DoctorService
                .GetAllByIdsAsync(Ids, HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
        // GET: api/<DoctorController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<DoctorController>/5


        //// POST api/<DoctorController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<DoctorController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<DoctorController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
