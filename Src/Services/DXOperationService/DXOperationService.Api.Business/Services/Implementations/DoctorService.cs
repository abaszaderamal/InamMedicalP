using DXOperationService.Api.Business.Services.Interfaces;
using DXOperationService.Api.Core.Abstracts;
using Med.Shared.Dtos;
using Med.Shared.Entities;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Med.Shared.Dtos.Doctor;
using Med.Shared.Extensions;

namespace DXOperationService.Api.Business.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<List<Doctor>>> GetAllAsync()
        {
            var result = await _unitOfWork.DoctorRepository.GetAllAsync(p => p.IsDeleted == false);
            return Response<List<Doctor>>.Success(result, 200);


        }
        public async Task<Response<List<DoctorDto2>>> GetAllByIdsAsync(string Ids , string userId)
        {
            List<DoctorDto2> resultDoctors = await _unitOfWork.DoctorRepository.GetAllByIdsAsync(Ids,userId);

            if (resultDoctors.Count == 0)
                return Response<List<DoctorDto2>>.Fail("Not Found", StatusCodes.Status404NotFound);

            return Response<List<DoctorDto2>>.Success(resultDoctors, StatusCodes.Status200OK);
        }


        public async Task<Response<Doctor>> GetByIdAsync(int id)
        {
            var result = await _unitOfWork
                .DoctorRepository
                .GetAsync(p => p.Id == id && p.IsDeleted == false);
            if (result is null)
            {
                return Response<Doctor>.Fail("Doctor not found.", StatusCodes.Status404NotFound);
            }
            return Response<Doctor>.Success(result, StatusCodes.Status200OK);
        }

         
    }
}
