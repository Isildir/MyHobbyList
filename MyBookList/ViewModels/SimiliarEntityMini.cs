using MyHobbyList.Models;

namespace MyHobbyList.ViewModels
{
    public class SimiliarEntityMini
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ElementType ElementType { get; set; }

        public int ImageId { get; set; }
    }

    public class SimiliarEntityMiniRecommend : SimiliarEntityMini
    {
        public string RecommenderEmail { get; set; }

        public string Message { get; set; }
    }
}