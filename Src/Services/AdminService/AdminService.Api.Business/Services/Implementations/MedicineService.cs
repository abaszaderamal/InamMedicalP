using AdminService.Api.Business.Services.Interfaces;
using AdminService.Api.Core.Abstracts;
using Med.Shared.Dtos;
using Med.Shared.Dtos.Clinic;
using Med.Shared.Dtos.Medicine;
using Med.Shared.Entities;
using Microsoft.AspNetCore.Http;

namespace AdminService.Api.Business.Services.Implementations
{
    public class MedicineService : IMedicineService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<List<Medicine>>> GetAllAsync()
        {
            var result = await _unitOfWork.MedRepository.GetAllAsync(p => p.IsDeleted == false);
            return Response<List<Medicine>>.Success(result, StatusCodes.Status200OK);
        }

        public async Task<Response<Medicine>> GetByIdAsync(int id)
        {
            var result = await _unitOfWork.MedRepository.GetAsync(p => p.IsDeleted == false && p.Id == id);
            if (result is null)
            {
                return Response<Medicine>.Fail("Medicine not found.", StatusCodes.Status404NotFound);
            }
            return Response<Medicine>.Success(result, StatusCodes.Status200OK);
        }

        public async Task<Response<NoContent>> CreateAsync(MedPostDto medPostDto)
        {
            Medicine medicine = new Medicine()
            {
                Name = medPostDto.Name,
                MedCategoryId = medPostDto.MedCategoryId,
            };

            await _unitOfWork.MedRepository.CreateAsync(medicine);
            await _unitOfWork.SaveAsync();
            return Response<NoContent>.Success(StatusCodes.Status200OK);
        }

        public async Task<Response<NoContent>> UpdateAsync(MedUpdateDto medUpdateDto)
        {
            Medicine medDb = await _unitOfWork.MedRepository.GetAsync(p => p.IsDeleted == false && p.Id == medUpdateDto.Id);
            if(medDb is null)  return Response<NoContent>.Fail("Medicine not found.", StatusCodes.Status404NotFound);

            medDb.MedCategoryId = medUpdateDto.MedCategoryId;
            medDb.Name = medUpdateDto.Name;

            _unitOfWork.MedRepository.Update(medDb);
            await _unitOfWork.SaveAsync();
            return Response<NoContent>.Success(StatusCodes.Status200OK);


        }

        public async Task<Response<NoContent>> DeleteAsync(int id)
        {
            Medicine medDb = await _unitOfWork.MedRepository.GetAsync(p => p.IsDeleted == false && p.Id == id);
            if (medDb is null) return Response<NoContent>.Fail("Medicine not found.", StatusCodes.Status404NotFound);

            medDb.IsDeleted = true;
            await _unitOfWork.SaveAsync();
            return Response<NoContent>.Success(StatusCodes.Status200OK);
        }

       
    }
}
