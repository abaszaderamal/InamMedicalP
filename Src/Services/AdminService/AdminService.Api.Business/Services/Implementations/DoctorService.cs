using AdminService.Api.Business.Services.Interfaces;
using AdminService.Api.Core.Abstracts;
using AdminService.Api.Data.DAL;
using Med.Shared.Dtos;
using Med.Shared.Dtos.Doctor;
using Med.Shared.Entities;
using Med.Shared.Extensions;
using Microsoft.AspNetCore.Http;

namespace AdminService.Api.Business.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
 


        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public async Task<Response<List<Doctor>>> GetAllAsync()
        {
            var result = await _unitOfWork.DoctorRepository.GetAllAsync(p => p.IsDeleted == false);
            return Response<List<Doctor>>.Success(result, StatusCodes.Status200OK);
        }

        public async Task<Response<Doctor>> GetByIdAsync(int id)
        {
            var result = await _unitOfWork.DoctorRepository.GetAsync(p => p.IsDeleted == false && p.Id == id);
            if (result is null)
            {
                return Response<Doctor>.Fail("Doctor not found.", StatusCodes.Status404NotFound);
            }
            return Response<Doctor>.Success(result, StatusCodes.Status200OK);
        }

        public async Task<Response<NoContent>> CreateAsync(DoctorClinicPostDto doctorPostDto)
        {
            Doctor doctor = new Doctor()
            {
                FirstName = doctorPostDto.FirstName,
                Email = doctorPostDto.Email,
                LastName = doctorPostDto.LastName,
                Number = doctorPostDto.Number,
                SpecialityId = doctorPostDto.SpecialityId,
                TagId = doctorPostDto.TagId,
                
            };

            await _unitOfWork.DoctorRepository.CreateAsync(doctor);
            await _unitOfWork.SaveAsync();
            if (doctorPostDto.ClinicsIds is null)
            {
                return Response<NoContent>.Success(StatusCodes.Status200OK);
            }

            List<int> ClinicIds = doctorPostDto.ClinicsIds.SplitToIntList();
            foreach (var id in ClinicIds)
            {
                var c = new ClinicDoctor() { DoctorId = doctor.Id, ClinicId = id, CreatedAt = DateTime.UtcNow };

                await _unitOfWork.ClinicDoctorRepository.CreateAsync(c);
                

            }

            await _unitOfWork.SaveAsync();
            return Response<NoContent>.Success(StatusCodes.Status200OK);

        }

        public async Task<Response<NoContent>> UpdateAsync(DoctorUpdateDto doctorUpdateDto)
        {
            Doctor doctorDb = await _unitOfWork.DoctorRepository.GetAsync(p=>p.IsDeleted==false && p.Id == doctorUpdateDto.Id);
            if (doctorDb is null) return Response<NoContent>.Fail("Doctor not found", StatusCodes.Status400BadRequest);

            doctorDb.FirstName = doctorUpdateDto.FirstName;
            doctorDb.LastName = doctorUpdateDto.LastName;
            doctorDb.Email = doctorUpdateDto.Email;
            doctorDb.Number = doctorUpdateDto.Number;
            doctorDb.SpecialityId = doctorUpdateDto.SpecialityId;
            doctorDb.TagId = doctorUpdateDto.TagId;

            await _unitOfWork.DoctorRepository.CreateAsync(doctorDb);
            await _unitOfWork.SaveAsync();
            if (doctorUpdateDto.ClinicsIds is null)
            {
                return Response<NoContent>.Success(StatusCodes.Status200OK);
            }

            List<int> ClinicIds = doctorUpdateDto.ClinicsIds.SplitToIntList();
            foreach (var id in ClinicIds)
            {
                var c = new ClinicDoctor() { DoctorId = doctorDb.Id, ClinicId = id, CreatedAt = DateTime.UtcNow };

                await _unitOfWork.ClinicDoctorRepository.CreateAsync(c);


            }

            await _unitOfWork.SaveAsync();
            return Response<NoContent>.Success(StatusCodes.Status200OK);

        }

        public async Task<Response<NoContent>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }


    }
}
