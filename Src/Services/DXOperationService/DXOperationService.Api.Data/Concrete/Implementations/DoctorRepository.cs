using DXOperationService.Api.Core.Abstracts.Repositores;
using DXOperationService.Api.Data.DAL;
using Med.Shared.Dtos.Clinic;
using Med.Shared.Dtos.Doctor;
using Med.Shared.Dtos.DXOperation;
using Med.Shared.Dtos.Medicine;
using Med.Shared.Entities;
using Med.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DXOperationService.Api.Data.Concrete.Implementations
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {

        private readonly AppDbContext _context;
        public DoctorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        //public async Task<List<Doctor>> GetAllAsync(Expression<Func<Doctor, bool>> expression)
        //{
        //    return await _context
        //        .Doctors
        //        .AsNoTracking()
        //        .Where(expression)
        //        .Include(p => p.Speciality)
        //        .Include(p => p.Tag)
        //        .Include(p => p.ClinicDoctors)
        //        .ThenInclude(p => p.Clinic)
        //        .ToListAsync();

        //}

        //public async Task<Doctor> GetAsync(Expression<Func<Doctor, bool>> expression)
        //{
        //    return await _context
        //        .Doctors
        //        .AsNoTracking()
        //        .Where(expression)
        //        .Include(p => p.Speciality)
        //        .Include(p => p.Tag)
        //        .Include(p => p.ClinicDoctors)
        //        .ThenInclude(p => p.Clinic)
        //        .FirstOrDefaultAsync();
        //}

        public async Task<List<DoctorDto2>> GetAllByIdsAsync(string Ids, string userId)
        {
            List<int> ids = Ids.SplitToIntList();

            #region DoctorDto
            List<DoctorDto2> doctorDtos = await _context
                .Doctors
                .Where(p => p.IsDeleted == false && ids.Contains(p.Id))
                .Include(p => p.Speciality)
                .Include(p => p.Tag)
                .Select(c => new DoctorDto2()
                {

                    Id = c.Id,
                    //Email = c.Email,
                    FirstName = c.FirstName,
                    LastName = c.LastName
                    //Number = c.Number,
                    //SpecialityName = c.Speciality.Name,
                    //SpecialityShortName = c.Speciality.ShortName,
                    //TagRaitingName = c.Tag.RaitingName,


                })
                .AsNoTracking()
                .ToListAsync();
            #endregion


            #region ClinicDocotor

            //List<ClinicDoctor> clinicDocs = await _context
            //    .ClinicDoctors
            //    .Where(p => p.IsDeleted == false && ids.Contains(p.DoctorId))
            //    .Include(p => p.Clinic)
            //    .AsNoTracking()
            //    .ToListAsync();

            #endregion

            #region DxOperation

            var dxOperations = await _context
                .DXOperations
                .Where(p => p.IsDeleted == false && p.AppUserId == userId)
                .Include(p => p.DXOperationMedicines)
                .Select(p => new DXOperationDto()
                {
                    Id = p.Id,
                    DoctorId = p.DoctorId,
                    //AppUserId = p.AppUserId,
                    Note = p.Note,
                    Status = p.Status,
                    MedDtos = p.DXOperationMedicines
                        .Select(m => new MedDto()
                        {
                            Id = m.Medicine.Id,
                            Name = m.Medicine.Name

                        }).ToList()
                })
                .ToListAsync();

            #endregion

            foreach (var doctorDto in doctorDtos)
            {
                //doctorDto.ClinicDtos = clinicDocs
                //    .Where(p => p.DoctorId == doctorDto.Id)
                //    .Select(p => new ClinicDto()
                //    {
                //        ClinicAddress = p.Clinic.Address,
                //        ClinicName = p.Clinic.Name,
                //        ClinicEmail = p.Clinic.Email,
                //        ClinicId = p.Clinic.Id,
                //        ClinicNumber = p.Clinic.Number
                //    }).ToList();
                doctorDto.DxOperationDtos = dxOperations
                    .Where(p => p.DoctorId == doctorDto.Id)
                    .ToList();
            }


            return doctorDtos;


            //foreach (int id in ids)
            //{
            //    DoctorDto doctorDto = new DoctorDto();
            //    if (await _context.ClinicDoctors.AnyAsync(p => p.DoctorId == id))
            //    {
            //        doctorDto = await _context.Doctors
            //            .Where(p => p.Id == id)
            //            .Include(p => p.Speciality)
            //            .Include(p => p.Tag)
            //            .Include(p => p.ClinicDoctors)
            //            .ThenInclude(p => p.Clinic)
            //            .SelectMany(p => p.ClinicDoctors, (c, i) => new DoctorDto()
            //            {

            //                Id = c.Id,
            //                Email = c.Email,
            //                FirstName = c.FirstName,
            //                LastName = c.LastName,
            //                Number = c.Number,
            //                SpecialityName = c.Speciality.Name,
            //                SpecialityShortName = c.Speciality.ShortName,
            //                TagRaitingName = c.Tag.RaitingName,


            //                ClinicDtos = c.ClinicDoctors.Select(c => new ClinicDto()
            //                {
            //                    ClinicAddress = c.Clinic.Address,
            //                    ClinicName = c.Clinic.Name,
            //                    ClinicEmail = c.Clinic.Email,
            //                    ClinicId = c.Clinic.Id,
            //                    ClinicNumber = c.Clinic.Number
            //                }).ToList()

            //            })
            //            .AsNoTracking()
            //            .FirstOrDefaultAsync();
            //        resultDoctors.Add(doctorDto);
            //    }
            //    else
            //    {
            //        doctorDto = await _context.Doctors
            //            .Where(p => p.Id == id)
            //            .Include(p => p.Speciality)
            //            .Include(p => p.Tag)
            //            .Include(p => p.ClinicDoctors)
            //            .ThenInclude(p => p.Clinic)
            //            .Select(p =>new DoctorDto()
            //            {

            //                Id = p.Id,
            //                Email = p.Email,
            //                FirstName = p.FirstName,
            //                LastName = p.LastName,
            //                Number = p.Number,
            //                SpecialityName = p.Speciality.Name,
            //                SpecialityShortName = p.Speciality.ShortName,
            //                TagRaitingName = p.Tag.RaitingName,
            //            })
            //            .AsNoTracking()
            //            .FirstOrDefaultAsync();
            //        resultDoctors.Add(doctorDto);
            //    }

            //}
            //return resultDoctors;
            //resultDoctors.Add(doctor);
            //resultDoctors
            //    .Add(await _unitOfWork
            //        .DoctorRepository
            //        .GetAsync(p => p.Id == id && p.IsDeleted == false));


            //if (resultDoctors.Count == 0)
            //    return Response<List<Doctor>>.Fail("Not Found", StatusCodes.Status404NotFound);

            //return Response<List<Doctor>>.Success(resultDoctors, StatusCodes.Status200OK);
        }
    }
}
