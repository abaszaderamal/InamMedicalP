using System.Security.Claims;
using DXOperationService.Api.Business.Services.Interfaces;
using Med.Shared.ControllerBases;
using Med.Shared.Dtos;
using Med.Shared.Dtos.Medicine;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DXOperationService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : CMControllerBase
    {
        private readonly IServiceUnitOfWork _serviceUnitOfWork;
        //private readonly HttpContextAccessor _httpContextAccessor;


        public MedicineController(IServiceUnitOfWork serviceUnitOfWork/*,HttpContextAccessor contextAccessor*/)
        {
            //_httpContextAccessor = contextAccessor;
            _serviceUnitOfWork = serviceUnitOfWork;

        }
        /// <summary>
        /// usere aid olan dermanlar
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin,ProjectManager,GroupManager,Member")]
        public async Task<Response<List<MedDto>>> Get()
        {



            return await _serviceUnitOfWork
                .MedicineService
                .GetUserMedicinesAsync(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        }
    }
}
