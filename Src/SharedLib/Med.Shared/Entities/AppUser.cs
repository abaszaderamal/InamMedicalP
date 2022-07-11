using Med.Shared.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace Med.Shared.Entities
{
    public class AppUser : IdentityUser,IAuditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? RefreshToken { get; set; }
        public int AvgValue{ get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public string? ProjectManagerId { get; set; }
        public string? GroupManagerId { get; set; }
        public bool IsDeleted { get; set; }



        //Navigation Property
        public ICollection<DXOperation> DXOperations{ get; set; }
        public ICollection<Todo> Todos{ get; set; }
        public ICollection<Evaluation> Evaluations{ get; set; }
        public ICollection<UserMedicine> UserMedicines { get; set; }



    }
}
