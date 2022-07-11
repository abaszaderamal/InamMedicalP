using DXOperationService.Api.Core.Abstracts;
using DXOperationService.Api.Core.Abstracts.Repositores;
using DXOperationService.Api.Data.Concrete.Implementations;
using DXOperationService.Api.Data.DAL;

namespace DXOperationService.Api.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;


        private IDXOperationRepository _dXOperationRepository;
        private IDoctorRepository _doctorRepository;
        private IMedicineRepository _medicineRepository;
        private IDXOperationMedicineRepository _dxOperationMedicineRepository;



        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }


        public IDXOperationRepository DXOperationRepository =>
            _dXOperationRepository ??= new DXOperationRepository(_context);


        public IMedicineRepository MedicineRepository => _medicineRepository ??= new MedicineRepository(_context);
        public IDXOperationMedicineRepository DxOperationMedicineRepository => _dxOperationMedicineRepository ??= new DXOperationMedicineRepository(_context);

        public IDoctorRepository DoctorRepository => _doctorRepository ??= new DoctorRepository(_context);


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
