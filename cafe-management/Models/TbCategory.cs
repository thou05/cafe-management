using System.ComponentModel.DataAnnotations;

namespace cafe_management.Models
{
    public class TbCategory
    {
        [Key] 
        public int Id { get; set; } 

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Category name must be between 3 and 100 characters.")]
        public string Name { get; set; } = null!; 

        public virtual ICollection<TbProduct> Products { get; set; } = new List<TbProduct>();
    }
}
