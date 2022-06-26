using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [DisplayName("display order")]
    [Range(1,100, ErrorMessage = "Display Order must be between 1 and 100.")]
    public int DisplayOrderType { get; set; }
    
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}