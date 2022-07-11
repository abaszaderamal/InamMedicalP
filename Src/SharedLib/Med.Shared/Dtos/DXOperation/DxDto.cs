using Med.Shared.Dtos.Doctor;
using Med.Shared.Dtos.Medicine;

namespace Med.Shared.Dtos.DXOperation
{
    public class DxDto
    {
        public List<DoctorDto2> Doctors{ get; set; }
        public List<MedDto> Medicines { get; set; }
    }
}
