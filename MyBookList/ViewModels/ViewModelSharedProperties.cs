using System;

namespace MyHobbyList.ViewModels
{
    public class ViewModelSharedProperties
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string GenreName { get; set; }

        public int ImageId { get; set; }

        public string Description { get; set; }
    }
}