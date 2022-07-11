using Med.Shared.Entities;

namespace Med.Shared.Dtos.DXOperation
{
    public class DXOperationPostDto
    {
        //public int Id { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public string AppUserId { get; set; }
        public int DoctorId { get; set; }
        public int MedicineId { get; set; }
    }
}
