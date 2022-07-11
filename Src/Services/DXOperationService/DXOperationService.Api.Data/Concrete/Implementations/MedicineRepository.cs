using DXOperationService.Api.Core.Abstracts.Repositores;
using DXOperationService.Api.Data.DAL;
using Med.Shared.Dtos.Medicine;
using Med.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace DXOperationService.Api.Data.Concrete.Implementations
{
    public class MedicineRepository : Repository<Medicine>, IMedicineRepository
    {
        //private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public MedicineRepository(AppDbContext context /*, UserManager<AppUser> userManager*/) : base(context)
        {
            //_userManager = userManager;
            _context = context;
        }

        public async Task<List<MedDto>> GetUserMedicinesAsync(string UserId)
        {
            List<MedDto> medicines = await _context
                .UserMedicines
                .Where(p => p.AppUserId == UserId && p.IsDeleted == false)
                .Include(p => p.Medicine).ThenInclude(p => p.MedCategory)
                .Where(p=>p.Medicine.IsDeleted==false)
                .Select(p => new MedDto()
                {
                    Name = p.Medicine.Name,
                    Id = p.MedicineId,
                    //CatName = p.Medicine.MedCategory.Name
                })
                .AsNoTracking()
                .ToListAsync();
            return medicines;



            //...





            //var user = await _context
            //    .Users
            //    .Where(p => p.Id == UserId && p.IsDeleted == false)
            //    .Include(p => p.UserMedicines).ThenInclude(p => p.Medicine).ThenInclude(p=>p.MedCategory).FirstOrDefaultAsync();
            //var result = user.UserMedicines.ToList();
            //List<Medicine> medicines = new List<Medicine>();
            //foreach (var UserMedicine in result)
            //{

            //    UserMedicine.Medicine.UserMedicines = null;
            //    UserMedicine.Medicine.DXOperationMedicine = null;


            //    medicines.Add(UserMedicine.Medicine);
            //}            //var result = user.UserMedicines.ToList();
            //List<Medicine> medicines = new List<Medicine>();
            //foreach (var UserMedicine in result)
            //{

            //    UserMedicine.Medicine.UserMedicines = null;
            //    UserMedicine.Medicine.DXOperationMedicine = null;


            //    medicines.Add(UserMedicine.Medicine);
            //}

        }
    }
}
