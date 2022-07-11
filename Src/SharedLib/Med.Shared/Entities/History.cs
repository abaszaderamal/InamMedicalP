namespace Med.Shared.Entities
{
    public class History
    {
        public int Id { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }
        //public AppUser UserId { get; set; }
    }
}
