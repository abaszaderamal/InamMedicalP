using Med.Shared.Abstracts;

namespace Med.Shared.Entities
{
    public class Evaluation : IAuditable
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        //Oy veren
        public string AppUserId { get; set; }
        public string VoterRole { get; set; }

        //    public AppUser AppUser { get; set; }

        // Sahibi
        public string OwnerUserId { get; set; }
        public AppUser OwnerUser { get; set; }
        public DateTime CreatedAt { get; set; }
        //public string Sv { get; set; }

        public ICollection<EvaluationRating> EvaluationRatings { get; set; }
    }
}


//[
//    {
//        evoId: 1,
//        ratindId: 2,
//        value: 3
//    },
//    {
//evoId: 1,
//        ratindId: 3,
//        value: 3
//    }
//{
//evoId: 1,
//        ratindId: 4,
//        value: 3
//    }
//]