using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FPTBook.Models;

public class CategoryRequest
{
    public int Id { get; set; }
    [DisplayName("Category")]
    public string Name { get; set; }
    public bool Is_Approved { get; set; }
    public DateTime CreatedAt { get; set; }

    public CategoryRequest()
    {
        CreatedAt = DateTime.Now;
    }
}