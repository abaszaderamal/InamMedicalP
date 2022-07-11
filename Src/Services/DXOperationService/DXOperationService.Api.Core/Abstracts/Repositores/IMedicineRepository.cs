using Med.Shared.Dtos.Medicine;
using Med.Shared.Entities;

namespace DXOperationService.Api.Core.Abstracts.Repositores
{
    public interface IMedicineRepository:IRepository<Medicine>
    {
        Task<List<MedDto>> GetUserMedicinesAsync(string UserId);
    }
}
