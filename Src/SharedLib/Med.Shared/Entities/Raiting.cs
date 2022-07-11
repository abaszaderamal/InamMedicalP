using Med.Shared.Abstracts;

namespace Med.Shared.Entities
{
    public class Raiting : IAuditable
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<EvaluationRating> EvaluationRatings { get; set; }

    }
}
