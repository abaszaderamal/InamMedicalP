using System.Security.Claims;
using DXOperationService.Api.Business.Services.Interfaces;
using Med.Shared.Dtos;
using Med.Shared.Dtos.DXOperation;
using Med.Shared.Entities;
using Med.Shared.HttpStatusCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DXOperationService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DXOperationController : ControllerBase
    {
        private readonly IServiceUnitOfWork _serviceUnitOfWork;

        public DXOperationController(IServiceUnitOfWork serviceUnitOfWork)
        {
            _serviceUnitOfWork = serviceUnitOfWork;
        }
        [HttpGet("Ok")]
        public IActionResult Ok()
        {

            return Ok("Okeydir Shamil");
        }

        [HttpPost("GetByIds")]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]
        public async Task<Response<DxDto>> Get([FromBody]string Ids)
        {
            var Meds= await _serviceUnitOfWork
                .MedicineService
                .GetUserMedicinesAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var Docs = await _serviceUnitOfWork
                .DoctorService
                .GetAllByIdsAsync(Ids, HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var res = new DxDto()
            {
                Doctors = Docs.Data,
                Medicines = Meds.Data
            };
            

            return Response<DxDto>.Success(res,200);



            //return await _serviceUnitOfWork.DxOperationService.GetAllWithDocidsAsync(Ids, HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);


        }

        //// GET api/<DXOperationController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        [HttpPost]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]
        public async Task<Response<NoContent>> Post([FromBody] DXOperationPostDto dxOperationPostDto)
        {
            if (!ModelState.IsValid) return Response<NoContent>.Fail("Validation Problem", CStatusCodes.Status1017ValidationProblem);

            dxOperationPostDto.AppUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

           return await _serviceUnitOfWork.DxOperationService.CreateAsync(dxOperationPostDto);
        }

        [HttpPut]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]

        public async Task<Response<NoContent>> Put([FromBody] DXOperationPutDto  dxOperationPutDto)
        {
           if (!ModelState.IsValid) return Response<NoContent>.Fail("Validation Problem", CStatusCodes.Status1017ValidationProblem);

           return await _serviceUnitOfWork.DxOperationService.UpdateAsync(dxOperationPutDto);
        }

        //// DELETE api/<DXOperationController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
