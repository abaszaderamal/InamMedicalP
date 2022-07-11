using Med.Shared.Abstracts;

namespace Med.Shared.Entities
{
    public class Clinic:IAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }

        //Navigation Property
        public ICollection<ClinicDoctor> ClinicDoctors { get; set; }

        public int RegionId { get; set; }
        public Region Region { get; set; }


    }
}
