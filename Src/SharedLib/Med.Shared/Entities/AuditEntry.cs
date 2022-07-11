using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Med.Shared.Entities
{
    public class AuditEntry
    {
        [Key]
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string ActionType { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string EntityId { get; set; }
        public Dictionary<string, object> Changes { get; set; }

        [NotMapped]
      
        public List<PropertyEntry> TempProperties { get; set; }
    }
}
