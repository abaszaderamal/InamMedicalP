using AutoMapper;
using EvaluationService.Api.Abstracts;
using EvaluationService.Api.Abstracts.Repositories;
using EvaluationService.Api.Concretes.Implementation;
using EvaluationService.Api.DAL;
using Med.Shared.Entities;
using Microsoft.AspNetCore.Identity;

namespace EvaluationService.Api.Concretes
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _context { get; set; }

        private IRaitingRepository _raitingRepository;
        private IEvaluationRepository _evaluationRepository;
        private IEvaluationRatingRepository _evaluationRatingRepository;
        private UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UnitOfWork(AppDbContext context, UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }



        public IRaitingRepository RaitingRepository => _raitingRepository ??= new RaitingRepository(_context);
        public IEvaluationRepository EvaluationRepository => _evaluationRepository ??= new EvaluationRepository(_context, _userManager, _mapper);

        public IEvaluationRatingRepository EvaluationRatingRepository =>
            _evaluationRatingRepository ??= new EvaluationRatingRepository(_context);

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
