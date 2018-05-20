using System.Collections.Generic;

namespace MyHobbyList.Models
{
    public class UserData
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public string Email { get; set; }

        public AccountType AccountType { get; set; }
        public AccountState AccountState { get; set; }

        public virtual List<Score> Scores { get; set; }
        public virtual List<Entity> Entities { get; set; }
        public virtual List<Recommend> Reccomendations { get; set; }

        public UserData() { }

        public UserData(string userId, string email) : base()
        {
            UserId = userId;
            Email = email;
            Scores = new List<Score>();
            Entities = new List<Entity>();
            Reccomendations = new List<Recommend>();
        }
    }
}