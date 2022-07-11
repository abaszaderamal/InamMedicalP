using Med.Shared.Abstracts;

namespace Med.Shared.Entities
{
    public class Todo : IAuditable
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Note { get; set; }
        public bool Status{ get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        //Navigation Property
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }
}
