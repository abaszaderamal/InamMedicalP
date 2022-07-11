using FluentValidation;
using Med.Shared.Dtos.DXOperation;

namespace Med.Shared.Validators.DXOperation
{
    public class DXOperationPostDtoValidator : AbstractValidator<DXOperationPostDto>
    {
        public DXOperationPostDtoValidator()
        {
            //RuleFor(p=>p.AppUserId).NotEmpty().NotNull();
            RuleFor(p => p.DoctorId).NotEmpty().NotNull();
            RuleFor(p => p.MedicineId).NotEmpty().NotNull();
            RuleFor(p => p.DoctorId).NotEmpty().NotNull();
            RuleFor(p => p.Note).NotEmpty().NotNull();
            RuleFor(p => p.Status).NotEmpty().NotNull();



        }
    }
}




