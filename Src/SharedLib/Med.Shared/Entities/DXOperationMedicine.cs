using Med.Shared.Abstracts;

namespace Med.Shared.Entities
{
    public class DXOperationMedicine : IAuditable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        //Navigation Property

        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; }


        public int DXOperationId { get; set; }
        public DXOperation DXOperation { get; set; }
    }
}
