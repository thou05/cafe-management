using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cafe_management.Models
{
    public class TbFeedback
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 100 characters.")]
        public string Title { get; set; } = null!; 

        [StringLength(15)]
        [Phone(ErrorMessage = "Invalid phone number format.")] 
        [Display(Name = "Phone Number")] 
        public string? PhoneNumber { get; set; } 

        [StringLength(2000, ErrorMessage = "Content cannot exceed 2000 characters.")]
        public string? Content { get; set; }
    }
}
