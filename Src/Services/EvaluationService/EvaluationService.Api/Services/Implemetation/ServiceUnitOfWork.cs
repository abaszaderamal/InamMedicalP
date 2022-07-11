using AutoMapper;
using EvaluationService.Api.Abstracts;
using EvaluationService.Api.Services.Interface;
using Med.Shared.Entities;
using Microsoft.AspNetCore.Identity;

namespace EvaluationService.Api.Services.Implemetation
{
    public class ServiceUnitOfWork : IServiceUnitOfWork
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;


        private IRaitingService _raitingService;
        private IEvaluationService _evaluationService;


        public ServiceUnitOfWork(IUnitOfWork unitOfWork ,UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        public IRaitingService RaitingService => _raitingService ??= new RaitingService(_unitOfWork);
        public IEvaluationService EvaluationService => _evaluationService ??= new EvaluationService(_unitOfWork,_userManager);
    }
}
