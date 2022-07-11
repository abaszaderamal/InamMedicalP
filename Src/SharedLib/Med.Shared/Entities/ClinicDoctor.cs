using System.ComponentModel.DataAnnotations.Schema;
using Med.Shared.Abstracts;

namespace Med.Shared.Entities
{
    public class ClinicDoctor : IAuditable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        //Navigation Property

        //[ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        //[ForeignKey("Clinic")]
        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }


    }
}
