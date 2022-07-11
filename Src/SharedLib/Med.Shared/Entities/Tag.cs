using Med.Shared.Abstracts;

namespace Med.Shared.Entities
{ 
    public class Tag : IAuditable
    {
        public int Id { get; set; }
        public string RaitingName { get; set; }
        public bool IsDeleted { get; set; }


        //Navigation Property
        public ICollection<Doctor> Doctors { get; set; }
    }
}
