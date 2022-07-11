using System.ComponentModel.DataAnnotations.Schema;
using Med.Shared.Abstracts;

namespace Med.Shared.Entities
{
    public class DXOperation : IAuditable
    {
        public int Id { get; set; }

        //Talked reminded
        public string Status { get; set; }
        public string Note { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }

        //Navigation Property
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }


        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public ICollection<DXOperationMedicine> DXOperationMedicines { get; set; }

    }
}
