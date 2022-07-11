using AutoMapper;
using EvaluationService.Api.Abstracts;
using EvaluationService.Api.Services.Interface;
using Med.Shared.Dtos;
using Med.Shared.Dtos.Raiting;
using Med.Shared.Entities;

namespace EvaluationService.Api.Services.Implemetation
{
    public class RaitingService : IRaitingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RaitingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<NoContent>> CreateAsync(RaitingPostDto PostDto)
        {
            Raiting raiting = new Raiting()
            {
                Title = PostDto.Title
            };
            await _unitOfWork.RaitingRepository.CreateAsync(raiting);
            await _unitOfWork.SaveAsync();

            return Response<NoContent>.Success(StatusCodes.Status200OK);
        }

        public Task<Response<NoContent>> UpdateAsync(RaitingUpdateDto updateDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<NoContent>> DeleteAsync(int id)
        {
            Raiting raiting = await _unitOfWork.RaitingRepository.GetAsync(p => p.Id == id && p.IsDeleted == false);
            if (raiting is null) return Response<NoContent>.Fail("Not Found", StatusCodes.Status404NotFound);
            raiting.IsDeleted = true;
            await _unitOfWork.SaveAsync();
            return Response<NoContent>.Success(StatusCodes.Status200OK);
            ;
        }

        public async Task<Response<List<RaitingDto>>> GetAllAsync()
        {
            List<Raiting> raitings = await _unitOfWork
                .RaitingRepository
                .GetAllAsync(p => p.IsDeleted == false);
            if (raitings is null) return Response<List<RaitingDto>>.Fail("Not Found", StatusCodes.Status404NotFound);
            List<RaitingDto> result = raitings.Select(p => new RaitingDto()
            {
                Id = p.Id,
                Title = p.Title
            }).ToList();
            return Response<List<RaitingDto>>.Success(result, StatusCodes.Status200OK);
        }

        public async Task<Response<RaitingDto>> GetByIdAsync(int id)
        {
            Raiting raiting = await _unitOfWork
                .RaitingRepository
                .GetAsync(p => p.Id == id && p.IsDeleted == false);
            if (raiting is null) return Response<RaitingDto>.Fail("Not Found", StatusCodes.Status404NotFound);

             
            return Response<RaitingDto>.Success(new RaitingDto()
            {
                Id = raiting.Id,
                Title = raiting.Title
            }, StatusCodes.Status200OK);
        }
    }
}
