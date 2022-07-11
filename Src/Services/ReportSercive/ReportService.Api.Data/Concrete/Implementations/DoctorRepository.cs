using System.Linq.Expressions;
using AutoMapper;
using Med.Shared.Dtos.Clinic;
using Med.Shared.Dtos.Doctor;
using Med.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using ReportService.Api.Core.Abstracts.Repositories;
using ReportService.Api.Data.DAL;

namespace ReportService.Api.Data.Concrete.Implementations
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DoctorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<DoctorDto>> GetAllAsync()
        {
            //var timer = new Stopwatch();
            //timer.Start();

            List<DoctorDto> doctorDtos = await _context
                .Doctors
                 .Where(p => p.IsDeleted == false)
                 .Include(p => p.Speciality)
                 .Include(p => p.Tag)
                 .Select(c => new DoctorDto()
                 {
                     Id = c.Id,
                     Email = c.Email,
                     FirstName = c.FirstName,
                     LastName = c.LastName,
                     Number = c.Number,
                     SpecialityName = c.Speciality.Name,
                     SpecialityShortName = c.Speciality.ShortName,
                     TagRaitingName = c.Tag.RaitingName,
                 })
                 .AsNoTracking()
                 .ToListAsync();


            List<ClinicDoctor> clinicDocs = await _context
                .ClinicDoctors
                .Where(p => p.IsDeleted == false)
                .Include(p => p.Clinic)
                .AsNoTracking()
                .ToListAsync();

            List<DoctorDto> result = new List<DoctorDto>();
            foreach (var doctorDto in doctorDtos)
            {
                var Clinics = clinicDocs
                    .Where(p => p.DoctorId == doctorDto.Id)
                    .Select(p => new ClinicDto()
                    {
                        ClinicAddress = p.Clinic.Address,
                        ClinicName = p.Clinic.Name,
                        ClinicEmail = p.Clinic.Email,
                        ClinicId = p.Clinic.Id,
                        ClinicNumber = p.Clinic.Number
                    }).ToList();
                if (Clinics.Count == 0)
                {
                    result.Add(doctorDto);
                    continue;
                }
                for (int i = 0; i < Clinics.Count; i++)
                {
                    var doc = new DoctorDto()
                    {
                        Id = doctorDto.Id,
                        FirstName = doctorDto.FirstName,
                        LastName = doctorDto.LastName,
                        Email = doctorDto.Email,
                        Number = doctorDto.Number,
                        SpecialityName = doctorDto.SpecialityName,
                        SpecialityShortName = doctorDto.SpecialityShortName,
                        TagRaitingName = doctorDto.TagRaitingName
                    };
                    doc.ClinicDto = Clinics[i];
                    result.Add(doc);
                }
            }







            return result;

            //var t = await (from doc in _context.Doctors
            //                   //join cd in _context.ClinicDoctors on doc.Id equals cd.DoctorId
            //               join spc in _context.Specialities on doc.SpecialityId equals spc.Id
            //               join tag in _context.Tag on doc.TagId equals tag.Id
            //               //join cl in _context.Clinic on cd.ClinicId equals cl.Id
            //               select new DoctorDto()
            //               {
            //                   Id = doc.Id,
            //                   FirstName = doc.FirstName,
            //                   LastName = doc.LastName,
            //                   Email = doc.Email,
            //                   Number = doc.Number,
            //                   SpecialityName = spc.Name,
            //                   SpecialityShortName = spc.ShortName,
            //                   TagRaitingName = tag.RaitingName,
            //                   //ClinicDtos = doc.ClinicDoctors.Count == 0 ? new List<ClinicDto>()
            //                   //    : doc.ClinicDoctors.Select(p => new ClinicDto()
            //                   //    {
            //                   //        ClinicAddress = p.Clinic.Address,
            //                   //        ClinicName = p.Clinic.Name,
            //                   //        ClinicEmail = p.Clinic.Email,
            //                   //        ClinicId = p.Clinic.Id,
            //                   //        ClinicNumber = p.Clinic.Number

            //                   //    }).ToList()

            //               }
            //    ) .AsNoTracking() .ToListAsync();
            //timer.Stop();

            //TimeSpan timeTaken = timer.Elapsed;
            //string foo = "----------------Time taken: " + timeTaken.ToString(@"m\:ss\.fff");
            //Console.WriteLine(foo);

            //for (int i = 0; i < 400; i++)
            //{
            //    await _context.Doctors.AddAsync(new Doctor()
            //    {
            //        FirstName = "sdfsdf",
            //        LastName = "sdfsf",
            //        Email = "dsfsdfsdf",
            //        Number = "sdfsdf",
            //        SpecialityId = 1,
            //        TagId = 1
            //    });
            //    Console.WriteLine(i);
            //}

            //await _context.SaveChangesAsync();

            //var ids = await _context
            //    .ClinicDoctors
            //    .Where(p => p.IsDeleted == false)
            //    .Select(p => new List<int>(){p.Id})
            //    .ToListAsync();
            //List<DoctorDto> doctorDtos = new List<DoctorDto>();


            //var k= await _context.Doctors
            //     .Where(p => p.IsDeleted == false && p.ClinicDoctors != null)
            //     .Include(p => p.Speciality)
            //     .Include(p => p.Tag)
            //     .Include(p => p.ClinicDoctors)
            //     .ThenInclude(p => p.Clinic)
            //     .SelectMany(p => p.ClinicDoctors, (c, i) => new DoctorDto()
            //     {

            //         Id = c.Id,
            //         Email = c.Email,
            //         FirstName = c.FirstName,
            //         LastName = c.LastName,
            //         Number = c.Number,
            //         SpecialityName = c.Speciality.Name,
            //         SpecialityShortName = c.Speciality.ShortName,
            //         TagRaitingName = c.Tag.RaitingName,


            //         //ClinicDtos = c.ClinicDoctors.Select(c => new ClinicDto()
            //         //{
            //         //    ClinicAddress = c.Clinic.Address,
            //         //    ClinicName = c.Clinic.Name,
            //         //    ClinicEmail = c.Clinic.Email,
            //         //    ClinicId = c.Clinic.Id,
            //         //    ClinicNumber = c.Clinic.Number
            //         //}).ToList()

            //     })
            //     .AsNoTracking()
            //     .ToListAsync();



            //doctorDtos.AddRange(await _context.Doctors
            //    .Where(p => p.IsDeleted == false && p.ClinicDoctors == null)
            //    .Include(p => p.Speciality)
            //    .Include(p => p.Tag)
            //    .Include(p => p.ClinicDoctors)
            //    .ThenInclude(p => p.Clinic)
            //    .Select(p => new DoctorDto()
            //    {

            //        Id = p.Id,
            //        Email = p.Email,
            //        FirstName = p.FirstName,
            //        LastName = p.LastName,
            //        Number = p.Number,
            //        SpecialityName = p.Speciality.Name,
            //        SpecialityShortName = p.Speciality.ShortName,
            //        TagRaitingName = p.Tag.RaitingName,
            //    })
            //    .AsNoTracking()
            //    .ToListAsync());
            //var m = k;



        }

        public async Task<DoctorDto> GetAsync(Expression<Func<Doctor, bool>> expression)
        {

            DoctorDto doctorDto = await _context
               .Doctors
               .Where(expression)
               .Include(p => p.Speciality)
               .Include(p => p.Tag)
               .Select(c => new DoctorDto()
               {

                   Id = c.Id,
                   Email = c.Email,
                   FirstName = c.FirstName,
                   LastName = c.LastName,
                   Number = c.Number,
                   SpecialityName = c.Speciality.Name,
                   SpecialityShortName = c.Speciality.ShortName,
                   TagRaitingName = c.Tag.RaitingName,
               })
               .AsNoTracking()
               .FirstOrDefaultAsync();


            doctorDto.ClinicDtos = await _context
                .ClinicDoctors
                .Where(p => p.IsDeleted == false && p.DoctorId == doctorDto.Id)
                .Include(p => p.Clinic)
                .AsNoTracking()
                .Select(p => new ClinicDto()
                {
                    ClinicAddress = p.Clinic.Address,
                    ClinicName = p.Clinic.Name,
                    ClinicEmail = p.Clinic.Email,
                    ClinicId = p.Clinic.Id,
                    ClinicNumber = p.Clinic.Number
                })
            .ToListAsync();


            return doctorDto;

            //ClinicDoctor  clinicDoc = await _context
            //    .ClinicDoctors
            //    .Where(p => p.IsDeleted == false)
            //    .Include(p => p.Clinic)
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync();

            //foreach (var doctorDto in doctorDtos)
            //{
            //    doctorDto.ClinicDtos = clinicDocs
            //        .Where(p => p.DoctorId == doctorDto.Id)
            //        .Select(p => new ClinicDto()
            //        {
            //            ClinicAddress = p.Clinic.Address,
            //            ClinicName = p.Clinic.Name,
            //            ClinicEmail = p.Clinic.Email,
            //            ClinicId = p.Clinic.Id,
            //            ClinicNumber = p.Clinic.Number
            //        }).ToList();
            //}


            //return await _context
            //    .Doctors
            //    .AsNoTracking()
            //    .Where(expression)
            //    .Include(p => p.Speciality)
            //    .Include(p => p.Tag)
            //    .Include(p => p.ClinicDoctors)
            //    .ThenInclude(p => p.Clinic)
            //    .FirstOrDefaultAsync();

            //return expression is null ? await _context.Doctors
            //    //.Where(p => p.IsDeleted == false)
            //    .Include(p => p.Speciality)
            //    .Include(p => p.Tag)
            //    .Include(p => p.ClinicDoctors)
            //    .ThenInclude(p => p.Clinic)
            //    .SelectMany(p => p.ClinicDoctors, (c, i) => new DoctorDto()
            //    {

            //        Id = c.Id,
            //        Email = c.Email,
            //        FirstName = c.FirstName,
            //        LastName = c.LastName,
            //        Number = c.Number,
            //        SpecialityName = c.Speciality.Name,
            //        SpecialityShortName = c.Speciality.ShortName,
            //        TagRaitingName = c.Tag.RaitingName,


            //        ClinicDtos = c.ClinicDoctors.Select(c => new ClinicDto()
            //        {
            //            ClinicAddress = c.Clinic.Address,
            //            ClinicName = c.Clinic.Name,
            //            ClinicEmail = c.Clinic.Email,
            //            ClinicId = c.Clinic.Id,
            //            ClinicNumber = c.Clinic.Number
            //        }).ToList()

            //    })
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync() :
            //    await _context.Doctors
            //        .Where(expression)
            //        .Include(p => p.Speciality)
            //        .Include(p => p.Tag)
            //        .Include(p => p.ClinicDoctors)
            //        .ThenInclude(p => p.Clinic)
            //        .SelectMany(p => p.ClinicDoctors, (c, i) => new DoctorDto()
            //        {

            //            Id = c.Id,
            //            Email = c.Email,
            //            FirstName = c.FirstName,
            //            LastName = c.LastName,
            //            Number = c.Number,
            //            SpecialityName = c.Speciality.Name,
            //            SpecialityShortName = c.Speciality.ShortName,
            //            TagRaitingName = c.Tag.RaitingName,


            //            ClinicDtos = c.ClinicDoctors.Select(c => new ClinicDto()
            //            {
            //                ClinicAddress = c.Clinic.Address,
            //                ClinicName = c.Clinic.Name,
            //                ClinicEmail = c.Clinic.Email,
            //                ClinicId = c.Clinic.Id,
            //                ClinicNumber = c.Clinic.Number
            //            }).ToList()

            //        })
            //        .AsNoTracking()
            //        .FirstOrDefaultAsync();

        }
    }
}
