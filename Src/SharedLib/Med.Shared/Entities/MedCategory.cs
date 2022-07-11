using Med.Shared.Abstracts;

namespace Med.Shared.Entities
{
    public class MedCategory : IAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool  IsDeleted { get; set; }

        //Navigation Property
        public ICollection<Medicine> Medicines{ get; set; }
    }
}
