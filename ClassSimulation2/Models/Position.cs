using ClassSimulation2.Models.Base;

namespace ClassSimulation2.Models
{
    public class Position:BaseEntity
    {
        public string Name {  get; set; }
        public List<Member> Members {  get; set; }
    }
}
