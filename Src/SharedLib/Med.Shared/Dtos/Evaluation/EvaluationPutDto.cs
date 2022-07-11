namespace Med.Shared.Dtos.Evaluation
{
    public class EvaluationPutDto
    {
        public int Id{ get; set; }
        public string OwnerUserId { get; set; }
        public int RatingId { get; set; }
        public int Value { get; set; }
    }
}
