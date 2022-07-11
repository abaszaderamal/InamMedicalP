using AdminService.Api.Business.Services.Interfaces;
using AdminService.Api.Core.Abstracts;
using Med.Shared.Dtos;
using Med.Shared.Dtos.Tag;
using Med.Shared.Entities;
using Microsoft.AspNetCore.Http;

namespace AdminService.Api.Business.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Response<List<Tag>>> GetAllAsync()
        {
            var result = await _unitOfWork.tagRepository.GetAllAsync(p => p.IsDeleted == false);
            return Response<List<Tag>>.Success(result, StatusCodes.Status200OK);
        }

        public async Task<Response<Tag>> GetByIdAsync(int id)
        {
            var result = await _unitOfWork.tagRepository.GetAsync(p => p.IsDeleted == false && p.Id == id);
            if (result is null)
            {
                return Response<Tag>.Fail("Tag not found.", StatusCodes.Status404NotFound);
            }
            return Response<Tag>.Success(result, StatusCodes.Status200OK);
        }
        public async Task<Response<NoContent>> CreateAsync(TagPostDto tagPostDto)
        {
            Tag tag = new Tag()
            {
                RaitingName =  tagPostDto.RaitingName
            };
            await _unitOfWork.tagRepository.CreateAsync(tag);
            await _unitOfWork.SaveAsync();
            return Response<NoContent>.Success(StatusCodes.Status200OK);
        }

        public async Task<Response<NoContent>> UpdateAsync(TagUpdateDto tagUpdateDto)
        {
            Tag tagDb = await _unitOfWork.tagRepository.GetAsync(p => p.IsDeleted == false && p.Id == tagUpdateDto.Id);
            if (tagDb is null) return Response<NoContent>.Fail("Tag is not found",StatusCodes.Status404NotFound);
            

            tagDb.RaitingName = tagUpdateDto.RaitingName;

            _unitOfWork.tagRepository.Update(tagDb);
            _unitOfWork.SaveAsync();
            return Response<NoContent>.Success(StatusCodes.Status200OK);
        }

        public async Task<Response<NoContent>> DeleteAsync(int id)
        {

            Tag tagDb = await _unitOfWork.tagRepository.GetAsync(p => p.IsDeleted == false && p.Id == id);
            if (tagDb is null) return Response<NoContent>.Fail("Tag is not found", StatusCodes.Status404NotFound);

            tagDb.IsDeleted = true;
            _unitOfWork.SaveAsync();
            return Response<NoContent>.Success(StatusCodes.Status200OK);


        }


    }
}
