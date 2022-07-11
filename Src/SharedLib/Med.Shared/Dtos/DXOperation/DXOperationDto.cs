using Med.Shared.Dtos.Medicine;

namespace Med.Shared.Dtos.DXOperation;

public class DXOperationDto
{
    public int Id { get; set; }
    public string Status { get; set; }
    public string Note { get; set; }
    //public string AppUserId { get; set; }
    public int DoctorId { get; set; }
    public List<MedDto>? MedDtos { get; set; }
}