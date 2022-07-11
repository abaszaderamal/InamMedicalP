using EvaluationService.Api.Services.Interface;
using Med.Shared.ControllerBases;
using Med.Shared.Dtos;
using Med.Shared.Dtos.Raiting;
using Med.Shared.Entities;
using Med.Shared.HttpStatusCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EvaluationService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaitingController : CMControllerBase
    {
        private readonly IServiceUnitOfWork _unitOfWork;

        public RaitingController(IServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }






        // GET: api/<RaitingController>
        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager")]
        public async Task<Response<List<RaitingDto>>> Get()
        {
            return await _unitOfWork.RaitingService.GetAllAsync();
        }

        // GET api/<RaitingController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager")]
        public async Task<Response<RaitingDto>> Get(int id)
        {
            return await _unitOfWork.RaitingService.GetByIdAsync(id);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager")]
        public async Task<Response<NoContent>> Post([FromBody] RaitingPostDto raitingPostDto)
        {
            if (!ModelState.IsValid) return Response<NoContent>.Fail("Validation Problem", CStatusCodes.Status1017ValidationProblem);

            if (ModelState.IsValid)
            {
                return await _unitOfWork.RaitingService.CreateAsync(raitingPostDto);
            }
            return Response<NoContent>.Fail("Valiation problem", StatusCodes.Status404NotFound);
        }

        // PUT api/<RaitingController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RaitingController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager")]
        public async Task<Response<NoContent>> Delete(int id)
        {
            return await _unitOfWork.RaitingService.DeleteAsync(id);
        }
    }
}
