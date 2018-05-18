using System.Collections.Generic;

namespace MyHobbyList.ViewModels
{
    public class UserProfileViewModel
    {
        public IEnumerable<BookIndexViewModel> BooksList { get; set; }

        public IEnumerable<MovieIndexViewModel> MoviesList { get; set; }

        public IEnumerable<GameIndexViewModel> GamesList { get; set; }

        public IEnumerable<SimiliarEntityMiniRecommend> RecommendedEntities { get; set; }
    }
}