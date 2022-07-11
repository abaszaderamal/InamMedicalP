using Med.Shared.Abstracts;

namespace Med.Shared.Entities
{
    public class EvaluationRating : IAuditable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }


        public int EvaluationId { get; set; }
        public Evaluation Evaluation { get; set; }
        public int RatingId { get; set; }
        public Raiting Raiting { get; set; }
        public int Value { get; set; }
    }
}
