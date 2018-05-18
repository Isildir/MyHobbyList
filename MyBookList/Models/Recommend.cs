using System.ComponentModel.DataAnnotations.Schema;

namespace MyHobbyList.Models
{
    public class Recommend
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public string FromUserEmail { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserData User { get; set; }

        public int? EntityId { get; set; }

        [ForeignKey("EntityId")]
        public virtual Entity Entity { get; set; }
        
        public ElementType ElementType { get; set; }

        public Recommend() { }

        public Recommend(int entityId, ElementType elementType)
        {
            EntityId = entityId;
            ElementType = elementType;
        }
    }
}