using System.Security.Claims;
using EvaluationService.Api.Services.Interface;
using Med.Shared.ControllerBases;
using Med.Shared.Dtos;
using Med.Shared.Dtos.Evaluation;
using Med.Shared.Dtos.User;
using Med.Shared.Entities;
using Med.Shared.HttpStatusCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EvaluationService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationController : CMControllerBase
    {
        private readonly IServiceUnitOfWork _unitOfWork;

        public EvaluationController(IServiceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // GET: api/<EvaluationController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<EvaluationController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<EvaluationController>
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager")]
        public async Task<Response<NoContent>> Post([FromBody] List<EvaluationPostDto> evaluationPostDto)
        {
            if (!ModelState.IsValid)
                return Response<NoContent>.Fail("Validation Problem", CStatusCodes.Status1017ValidationProblem);

            return await _unitOfWork.EvaluationService.CreateAsync(evaluationPostDto,
                HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }


        /// <summary>
        /// login olan userin qiymet vere bileceyi useri qaytarir
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager")]
        public async Task<Response<EvaluationRaitingDto>> GetAllUsers()
        {
            var c = await _unitOfWork.EvaluationService.GetAllUserAsync(
                HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var b = await _unitOfWork.RaitingService.GetAllAsync();

            var res = new EvaluationRaitingDto
            {
                Evaluations = c.Data,
                Raitings = b.Data
            };
            return Response<EvaluationRaitingDto>.Success(res, 200);
        }

        //// PUT api/<EvaluationController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<EvaluationController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}