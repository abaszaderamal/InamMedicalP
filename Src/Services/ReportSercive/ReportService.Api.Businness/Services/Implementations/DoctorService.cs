using Med.Shared.Dtos;
using Med.Shared.Dtos.Doctor;
using Med.Shared.Entities;
using Microsoft.AspNetCore.Http;
using ReportService.Api.Businness.Services.Interfaces;
using ReportService.Api.Core.Abstracts;

namespace ReportService.Api.Businness.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
//
        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task Create(DoctorClinicPostDto doctorPostDto)
        {
            throw new NotImplementedException();
        }

        public Task Update(int id, DoctorUpdateDto doctorUpdateDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<DoctorDto>>> GetAllAsync()
        {
            var result = await _unitOfWork.doctorRepository.GetAllAsync();
            return Response<List<DoctorDto>>.Success(result, StatusCodes.Status200OK);


        }

        public async Task<Response<DoctorDto>> GetByIdAsync(int id)
        {
            var result = await _unitOfWork
                .doctorRepository
                .GetAsync(p => p.Id == id && p.IsDeleted == false);
            if (result is null)
            {
                return Response<DoctorDto>.Fail("Doctor not found.", StatusCodes.Status404NotFound);
            }
            return Response<DoctorDto>.Success(result, StatusCodes.Status200OK);
        }
    }
}
