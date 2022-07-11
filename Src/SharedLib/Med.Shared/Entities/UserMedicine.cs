using System.ComponentModel.DataAnnotations.Schema;
using Med.Shared.Abstracts;

namespace Med.Shared.Entities
{
    public class UserMedicine : IAuditable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        //Navigation Property

        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }


        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }
    }
}
