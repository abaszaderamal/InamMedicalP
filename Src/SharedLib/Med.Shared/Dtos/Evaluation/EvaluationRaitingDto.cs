using Med.Shared.Dtos.Raiting;

namespace Med.Shared.Dtos.Evaluation;

public class EvaluationRaitingDto
{
    public List<EvaluationGetDto> Evaluations   { get; set; }
    public List<RaitingDto>  Raitings   { get; set; }

}