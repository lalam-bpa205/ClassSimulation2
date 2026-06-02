using ClassSimulation2.Models.Base;

namespace ClassSimulation2.Models
{
    public class Member:BaseEntity
    {
        public string Name {  get; set; }
        public string Surname {  get; set; }
        public string Description { get; set; }
        public Position Position { get; set; }
        public int PositionId {  get; set; }
        public string ImageUrl {  get; set; }
    }
}
