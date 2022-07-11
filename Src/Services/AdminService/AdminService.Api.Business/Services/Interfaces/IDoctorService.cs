using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Med.Shared.Dtos;
using Med.Shared.Dtos.Doctor;
using Med.Shared.Entities;

namespace AdminService.Api.Business.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<Response<NoContent>> CreateAsync(DoctorClinicPostDto doctorPostDto);
        Task<Response<NoContent>> UpdateAsync(DoctorUpdateDto doctorUpdateDto);
        Task<Response<NoContent>> DeleteAsync(int id);
        Task<Response<List<Doctor>>> GetAllAsync();
        Task<Response<Doctor>> GetByIdAsync(int id);
    }
}
