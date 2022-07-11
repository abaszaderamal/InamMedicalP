using EvaluationService.Api.Abstracts;
using EvaluationService.Api.Services.Interface;
using Med.Shared.Dtos;
using Med.Shared.Dtos.Evaluation;
using Med.Shared.Dtos.User;
using Med.Shared.Entities;
using Microsoft.AspNetCore.Identity;

namespace EvaluationService.Api.Services.Implemetation
{
    public class EvaluationService : IEvaluationService
    {
        // memberden basqa accses
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public EvaluationService(IUnitOfWork unitOfWork,UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<Response<List<EvaluationGetDto>>> GetAllUserAsync(string userId)
        {
            var user = await _unitOfWork.EvaluationRepository.GetAllUserAsync(userId);

            return user;
        }

        public async Task<Response<NoContent>> CreateAsync(List<EvaluationPostDto> PostDtos, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var userRole = await _userManager.GetRolesAsync(user);

            if (!(await _unitOfWork.EvaluationRepository.CheckRaitingStatus(userId)))
            {
                return Response<NoContent>.Fail("Bu ay uchun zanitdi", StatusCodes.Status400BadRequest);

            }
            Evaluation evaluation = new Evaluation()
            {
                AppUserId = userId,
                OwnerUserId = PostDtos[0].OwnerUserId,
                CreatedAt = DateTime.UtcNow,
                VoterRole = userRole[0]
            };
            await _unitOfWork.EvaluationRepository.CreateAsync(evaluation);
            await _unitOfWork.SaveAsync();

            foreach (var PostDto in PostDtos)
            {


                EvaluationRating evaluationRating = new EvaluationRating()
                {
                    CreatedAt = DateTime.UtcNow,
                    EvaluationId = evaluation.Id,
                    RatingId = PostDto.RatingId,
                    Value = PostDto.Value
                };
                await _unitOfWork.EvaluationRatingRepository.CreateAsync(evaluationRating);

            }
            await _unitOfWork.SaveAsync();

            await _unitOfWork.EvaluationRepository.SetAVGValue(PostDtos[0].OwnerUserId);

            //await _unitOfWork.EvaluationRepository.GetUserEvaluationByIdAsync(userId);

            return Response<NoContent>.Success(StatusCodes.Status200OK);

        }

        public Task<Response<NoContent>> UpdateAsync(EvaluationPutDto updateDto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<NoContent>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<Raiting>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }



        public Task<Response<Raiting>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
