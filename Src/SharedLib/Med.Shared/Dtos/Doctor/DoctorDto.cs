using Med.Shared.Dtos.Clinic;
using Med.Shared.Dtos.DXOperation;

namespace Med.Shared.Dtos.Doctor
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }



       // public int SpecialityId { get; set; }
        public string SpecialityName { get; set; }
        public string SpecialityShortName { get; set; }



        //public int TagId { get; set; }
        public string TagRaitingName { get; set; }



       public List<ClinicDto>? ClinicDtos { get; set; }
        public  ClinicDto  ClinicDto { get; set; }
        public List<DXOperationDto>? DxOperationDtos { get; set; }



}
    public class DoctorDto2
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string Number { get; set; }
        //public string Email { get; set; }



       // public int SpecialityId { get; set; }
        //public string SpecialityName { get; set; }
        //public string SpecialityShortName { get; set; }



        ////public int TagId { get; set; }
        //public string TagRaitingName { get; set; }



       //public List<ClinicDto>? ClinicDtos { get; set; }
        //public  ClinicDto  ClinicDto { get; set; }
        public List<DXOperationDto>? DxOperationDtos { get; set; }



}
}
