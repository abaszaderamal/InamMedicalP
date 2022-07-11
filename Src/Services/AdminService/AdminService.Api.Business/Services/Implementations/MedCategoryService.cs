using AdminService.Api.Business.Services.Interfaces;
using AdminService.Api.Core.Abstracts;
using Med.Shared.Dtos;
using Med.Shared.Dtos.MedCategory;
using Med.Shared.Entities;
using Microsoft.AspNetCore.Http;

namespace AdminService.Api.Business.Services.Implementations
{
    public class MedCategoryService : IMedCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Response<List<MedCategory>>> GetAllAsync()
        {
            var result = await _unitOfWork.MedCategoryRepository.GetAllAsync(p => p.IsDeleted == false);
            return Response<List<MedCategory>>.Success(result, StatusCodes.Status200OK);
        }

        public async Task<Response<MedCategory>> GetByIdAsync(int id)
        {
            var result = await _unitOfWork.MedCategoryRepository.GetAsync(p => p.IsDeleted == false && p.Id == id);
            if (result is null)
            {
                return Response<MedCategory>.Fail("Medicine Category not found.", StatusCodes.Status404NotFound);
            }
            return Response<MedCategory>.Success(result, StatusCodes.Status200OK);
        }

        public async Task<Response<NoContent>> CreateAsync(MedCatPostDto medCatPostDto)
        {
            MedCategory medCategory = new MedCategory()
            {
                Name = medCatPostDto.Name,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.MedCategoryRepository.CreateAsync(medCategory);
            await _unitOfWork.SaveAsync();
            return Response<NoContent>.Success(StatusCodes.Status200OK);

        }

        public async Task<Response<NoContent>> UpdateAsync( MedCatUpdateDto medCatUpdateDto)
        {
            MedCategory medCategoryDb = await _unitOfWork.MedCategoryRepository.GetAsync(p=>p.IsDeleted==false && p.Id == medCatUpdateDto.Id);
            if (medCategoryDb is null)
                return Response<NoContent>.Fail("Medicine Category is not found", StatusCodes.Status404NotFound);

            medCategoryDb.Name = medCatUpdateDto.Name;

            _unitOfWork.MedCategoryRepository.Update(medCategoryDb);
            await _unitOfWork.SaveAsync();
            return Response<NoContent>.Success(StatusCodes.Status200OK);
        }

        public async Task<Response<NoContent>> DeleteAsync(int id)
        {
            MedCategory medCategoryDb = await _unitOfWork.MedCategoryRepository.GetAsync(p => p.IsDeleted == false && p.Id == id);
            if (medCategoryDb is null)
                return Response<NoContent>.Fail("Medicine Category is not found", StatusCodes.Status404NotFound);


            medCategoryDb.IsDeleted = true;
            await _unitOfWork.SaveAsync();
            return Response<NoContent>.Success(StatusCodes.Status200OK);
        }

      


    }
}
