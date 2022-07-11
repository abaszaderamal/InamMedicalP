using AdminService.Api.Business.Services.Interfaces;
using AdminService.Api.Core.Abstracts;
using Med.Shared.Dtos;
using Med.Shared.Dtos.Speciality;
using Med.Shared.Entities;
using Microsoft.AspNetCore.Http;

namespace AdminService.Api.Business.Services.Implementations
{
    public class SpecialityService : ISpecialityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpecialityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<List<Speciality>>> GetAllAsync()
        {
            var result = await _unitOfWork.specialityRepository.GetAllAsync(p => p.IsDeleted == false);
            return Response<List<Speciality>>.Success(result, StatusCodes.Status200OK);
        }

        public async Task<Response<Speciality>> GetByIdAsync(int id)
        {
            var result = await _unitOfWork.specialityRepository.GetAsync(p => p.IsDeleted == false && p.Id == id);
            if (result is null)
            {
                return Response<Speciality>.Fail("Speciality not found.", StatusCodes.Status404NotFound);
            }
            return Response<Speciality>.Success(result, StatusCodes.Status200OK);
        }

        public async Task<Response<NoContent>> CreateAsync(SpecialityPostDto specialityPostDto)
        {
            Speciality speciality = new Speciality()
            {
                Name = specialityPostDto.Name,
                ShortName = specialityPostDto.ShortName
            };
            await _unitOfWork.specialityRepository.CreateAsync(speciality);
            await _unitOfWork.SaveAsync();
            return Response<NoContent>.Success(StatusCodes.Status200OK);
        }

        public async Task<Response<NoContent>> UpdateAsync(SpecialityUpdateDto specialityUpdateDto)
        {
            Speciality specialityDb = await _unitOfWork.specialityRepository.GetAsync(p=>p.IsDeleted==false && p.Id == specialityUpdateDto.Id);
            if (specialityDb is null)  return Response<NoContent>.Fail("Speciality not found", StatusCodes.Status404NotFound);

            specialityDb.Name = specialityUpdateDto.Name;
            specialityDb.ShortName = specialityUpdateDto.ShortName;

            _unitOfWork.specialityRepository.Update(specialityDb);
            _unitOfWork.SaveAsync();
            return Response<NoContent>.Success(StatusCodes.Status200OK);
        }

        public async Task<Response<NoContent>> DeleteAsync(int id)
        {
            Speciality specialityDb = await _unitOfWork.specialityRepository.GetAsync(p => p.IsDeleted == false && p.Id == id);
            if (specialityDb is null) return Response<NoContent>.Fail("Speciality not found", StatusCodes.Status404NotFound);

            specialityDb.IsDeleted = true;
            _unitOfWork.SaveAsync();
            return Response<NoContent>.Success(StatusCodes.Status200OK);
        }

        
    }
}
