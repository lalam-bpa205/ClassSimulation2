using System.ComponentModel.DataAnnotations;

namespace ClassSimulation2.Areas.Admin.ViewModels.Position
{
    public record CreatePositionVM
    {
        [Required(ErrorMessage ="Name is required...")]
        [StringLength(25,ErrorMessage ="Name can not be exceed 25 characters")]
        [MinLength(3,ErrorMessage ="Name should be contain at least 3 characters")]
        public string Name {  get; set; }
    }
}
