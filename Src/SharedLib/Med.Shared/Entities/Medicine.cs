using Med.Shared.Abstracts;

namespace Med.Shared.Entities
{
    public class Medicine : IAuditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        //Navigation Property

        public int MedCategoryId { get; set; }
        public MedCategory MedCategory { get; set; }

        public ICollection<UserMedicine> UserMedicines { get; set; }
        public ICollection<DXOperationMedicine> DXOperationMedicine { get; set; }


    }
}
