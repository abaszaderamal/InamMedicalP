using Med.Shared.Dtos.Doctor;
using Med.Shared.Entities;

namespace DXOperationService.Api.Core.Abstracts.Repositores
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        //Task<List<Doctor>> GetAllAsync(Expression<Func<Doctor, bool>> expression = null);
        //Task<Doctor> GetAsync(Expression<Func<Doctor, bool>> expression = null);
        Task<List<DoctorDto2>> GetAllByIdsAsync(string Ids, string userId);



    }
}
