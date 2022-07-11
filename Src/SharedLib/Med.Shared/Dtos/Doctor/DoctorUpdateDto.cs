namespace Med.Shared.Dtos.Doctor
{
    public class DoctorUpdateDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public int SpecialityId { get; set; }
        public int TagId { get; set; }
        public string ClinicsIds { get; set; }
    }
}
