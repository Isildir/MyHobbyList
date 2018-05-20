using MyHobbyList.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MyHobbyList.ViewModels
{
    public class EntityViewModelFormProperties : ViewModelSharedProperties
    {
        public int GenreId { get; set; }

        public bool IsNew { get; set; }
    }

    public class EntityViewModelIndexDetailsProperties : ViewModelSharedProperties
    {
        public short YourScore { get; set; } = 0;

        public double AverageScore { get; set; } = 0;
    }

    public class EntityViewModelDetailsProperties : EntityViewModelIndexDetailsProperties
    {
        public List<SimiliarEntityMini> SimiliarEntities { get; set; }

        public List<CommentViewModel> Comments { get; set; }

        public EntityViewModelDetailsProperties()
        {
            SimiliarEntities = new List<SimiliarEntityMini>();
            Comments = new List<CommentViewModel>();
        }
    }
}