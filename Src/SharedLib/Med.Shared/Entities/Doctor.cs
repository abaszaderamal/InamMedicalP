using Med.Shared.Abstracts;

namespace Med.Shared.Entities
{
    public class Doctor: IAuditable
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }



        //Navigation Property

        public int SpecialityId { get; set; }
        public Speciality Speciality { get; set; }


        public int TagId { get; set; }
        public Tag Tag{ get; set; }

        public ICollection<DXOperation> DXOperations{ get; set; }
        public ICollection<ClinicDoctor> ClinicDoctors { get; set; }
    }
}
