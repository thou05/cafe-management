using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cafe_management.Models
{
    public class TbNews
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(250, MinimumLength = 10, ErrorMessage = "Title must be between 10 and 250 characters.")]
        public string Title { get; set; } = null!; 

        [DataType(DataType.Date)] 
        [Display(Name = "Posted Date")] 
        public DateOnly? PostedDate { get; set; } 

        [DataType(DataType.MultilineText)]
        public string? Content { get; set; } 

        [StringLength(500)]
        [DataType(DataType.ImageUrl)] 
        [Display(Name = "Image")]
        public string? Image { get; set; }
    }
}
