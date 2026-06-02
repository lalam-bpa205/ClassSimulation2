using System.ComponentModel.DataAnnotations;

namespace ClassSimulation2.Areas.Admin.ViewModels.Member
{
    public record CreateMemberVM
    {

        [Required(ErrorMessage = "Name is required...")]
        [StringLength(25, ErrorMessage = "Name can not be exceed 25 characters")]
        [MinLength(3, ErrorMessage = "Name should be contain at least 3 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required...")]
        [StringLength(50, ErrorMessage = "Surname can not be exceed 50 characters")]
        [MinLength(3, ErrorMessage = "Surname should be contain at least 3 characters")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Description is required...")]
        [StringLength(300, ErrorMessage = "Description can not be exceed 300 characters")]
        [MinLength(3, ErrorMessage = "Description should be contain at least 3 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "PositionId is required...")]
        public int PositionId { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile ImageFile {  get; set; }
    }
}
