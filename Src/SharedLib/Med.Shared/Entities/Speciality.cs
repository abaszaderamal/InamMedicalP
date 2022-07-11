using Med.Shared.Abstracts;

namespace Med.Shared.Entities
{
    public class Speciality : IAuditable 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public bool IsDeleted { get; set; }


        //Navigation Property
        public ICollection<Doctor> Doctors { get; set; }

    }
}
