using System.Linq.Expressions;
using Med.Shared.Dtos.Doctor;
using Med.Shared.Entities;

namespace ReportService.Api.Core.Abstracts.Repositories
{
    public  interface IDoctorRepository : IRepository<Doctor>
    {
        Task<List<DoctorDto>> GetAllAsync();
        Task<DoctorDto> GetAsync(Expression<Func<Doctor, bool>> expression = null);


    }
}
