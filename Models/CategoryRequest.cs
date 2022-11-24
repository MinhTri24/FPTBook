using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FPTBook.Models;

public class CategoryRequest
{
    public int Id { get; set; }
    [DisplayName("User Id:")]
    [ForeignKey("ApplicationUser")]
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public string Content { get; set; }
    public bool Is_Approved { get; set; }
    public DateTime CreatedAt { get; set; }

    public CategoryRequest()
    {
        CreatedAt = DateTime.Now;
    }
}