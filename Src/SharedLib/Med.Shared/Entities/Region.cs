using Med.Shared.Abstracts;

namespace Med.Shared.Entities
{
    public class Region : IAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        
        // d Navigation Property
        public ICollection<Clinic> Clinics{ get; set; }
    }
}
