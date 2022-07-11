using DXOperationService.Api.Business.Services.Interfaces;
using DXOperationService.Api.Core.Abstracts;
using Med.Shared.Dtos;
using Med.Shared.Dtos.Medicine;
using Microsoft.AspNetCore.Http;

namespace DXOperationService.Api.Business.Services.Implementations
{
    public class MedicineService : IMedicineService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Response<List<MedDto>>> GetUserMedicinesAsync(string UserId)
        {
            List<MedDto> medicines = await _unitOfWork
                .MedicineRepository
                .GetUserMedicinesAsync(UserId);
            if (medicines.Count==0)
                return Response<List<MedDto>>.Fail("Not Found", StatusCodes.Status404NotFound);

            return Response<List<MedDto>>.Success(medicines, StatusCodes.Status200OK);
        }
    }
}
