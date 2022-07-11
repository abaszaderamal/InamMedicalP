using Med.Shared.Dtos;
using Med.Shared.Dtos.Medicine;

namespace DXOperationService.Api.Business.Services.Interfaces
{
    public interface IMedicineService
    {
        Task<Response<List<MedDto>>> GetUserMedicinesAsync(string UserId);
    }
}
