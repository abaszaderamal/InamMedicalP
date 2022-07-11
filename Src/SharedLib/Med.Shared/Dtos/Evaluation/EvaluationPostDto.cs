
using Med.Shared.Entities;

namespace Med.Shared.Dtos.Evaluation
{
    public class EvaluationPostDto
    {
        public string OwnerUserId { get; set; }
        public int RatingId { get; set; }
        public int Value { get; set; }

    }
}
