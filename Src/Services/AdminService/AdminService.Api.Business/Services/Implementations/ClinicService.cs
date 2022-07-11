using AdminService.Api.Business.Services.Interfaces;
using AdminService.Api.Core.Abstracts;
using AdminService.Api.Data.DAL;
using Med.Shared.Dtos;
using Med.Shared.Dtos.Clinic;
using Med.Shared.Dtos.Doctor;
using Med.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace AdminService.Api.Business.Services.Implementations
{
    public class ClinicService : IClinicService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClinicService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

         public async Task<Response<List<Clinic>>> GetAllAsync()
        {
            var result = await _unitOfWork.clinicRepository.GetAllAsync(p => p.IsDeleted == false);
            return Response<List<Clinic>>.Success(result, StatusCodes.Status200OK);
        }

         
        public async Task<Response<Clinic>> GetByIdAsync(int id)
        {
            var result = await _unitOfWork.clinicRepository.GetAsync(p => p.IsDeleted == false && p.Id == id);
            if (result is null)
            {
                return Response<Clinic>.Fail("Clinic not found.", StatusCodes.Status404NotFound);
            }
            return Response<Clinic>.Success(result, StatusCodes.Status200OK);


        }
        
        public async Task<Response<NoContent>> CreateAsync(ClinicPostDto clinicPostDto)
        {
            Clinic clinic = new Clinic
            {
                Name = clinicPostDto.Name,
                Email = clinicPostDto.Email,
                Address = clinicPostDto.Address,
                Number = clinicPostDto.Number
            };

            await _unitOfWork.clinicRepository.CreateAsync(clinic);
            await _unitOfWork.SaveAsync();
            return Response<NoContent>.Success(StatusCodes.Status200OK);

        }

        public Task UpdateAsync(int id, ClinicUpdateDto clinicUpdateDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

       
    }
}
