using DXOperationService.Api.Business.Services.Interfaces;
using DXOperationService.Api.Core.Abstracts;
using DXOperationService.Api.Data.DAL;
using Med.Shared.Dtos;
using Med.Shared.Dtos.DXOperation;
using Med.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DXOperationService.Api.Business.Services.Implementations
{
    public class DXOperationService : IDXOperationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public DXOperationService(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<Response<NoContent>> CreateAsync(DXOperationPostDto dxOperationPostDto)
        {


            var dxops = _context
                .DXOperations
                .Where(p => p.IsDeleted == false && p.DoctorId == dxOperationPostDto.DoctorId &&
                            p.AppUserId == dxOperationPostDto.AppUserId && p.CreatedAt.Date == DateTime.UtcNow.Date)
                .Include(p => p.DXOperationMedicines)
                .ToList();
            if (dxops != null && dxops.Count != 0)
            {

                foreach (var dxop in dxops)
                {
                    foreach (var medicine in dxop.DXOperationMedicines)
                    {
                        if (medicine.MedicineId == dxOperationPostDto.MedicineId &&
                            medicine.CreatedAt.Date == DateTime.UtcNow.Date)
                        {
                            return Response<NoContent>.Fail("bu gune  bu derman gosterilen hekim ile qeyde alinib",
                                StatusCodes.Status400BadRequest);
                        }
                    }
                }
            }


            //var isemp = await _unitOfWork
            //    .DxOperationMedicineRepository
            //    .GetAsync(p => p.CreatedAt.Date == DateTime.UtcNow.Date);
            //if (isemp != null)
            //{
            //    return Response<NoContent>.Fail("bu gune  bu derman gosterilen hekim ile qeyde alinib", StatusCodes.Status400BadRequest);
            //}

            DXOperation dxOperation = new DXOperation()
            {
                Status = dxOperationPostDto.Status,
                Note = dxOperationPostDto.Note,
                CreatedAt = DateTime.UtcNow,
                AppUserId = dxOperationPostDto.AppUserId,
                DoctorId = dxOperationPostDto.DoctorId
            };
            await _unitOfWork.DXOperationRepository.CreateAsync(dxOperation);
            await _unitOfWork.SaveAsync();


            DXOperationMedicine dxOperationMedicine = new DXOperationMedicine()
            {
                MedicineId = dxOperationPostDto.MedicineId,
                DXOperationId = dxOperation.Id,
                CreatedAt = DateTime.UtcNow
            };
            await _unitOfWork.DxOperationMedicineRepository.CreateAsync(dxOperationMedicine);
            await _unitOfWork.SaveAsync();

            return Response<NoContent>.Success(StatusCodes.Status200OK);
        }

        public async Task<Response<NoContent>> UpdateAsync(DXOperationPutDto dxOperationPutDto)
        {
            DXOperation dxOperation = await _unitOfWork
                .DXOperationRepository
                .GetAsync(p => p.Id == dxOperationPutDto.Id);

            if (dxOperation is null)
                return Response<NoContent>.Fail("Not Found", StatusCodes.Status404NotFound);
            dxOperation.Status = dxOperationPutDto.Status;
            dxOperation.Note = dxOperationPutDto.Note;
            _unitOfWork.DXOperationRepository.Update(dxOperation);
            await _unitOfWork.SaveAsync();
            return Response<NoContent>.Success(StatusCodes.Status200OK);
        }

        public async Task<Response<List<DXOperation>>> GetAllWithDocidsAsync(string Ids, string userId)
        {
            List<DXOperation> result = await _unitOfWork.DXOperationRepository.GetAllWithDocidsAsync(Ids, userId);
            if (result is null) return Response<List<DXOperation>>.Fail("not found", StatusCodes.Status404NotFound);

            return Response<List<DXOperation>>.Success(result, StatusCodes.Status200OK);
        }
    }
}