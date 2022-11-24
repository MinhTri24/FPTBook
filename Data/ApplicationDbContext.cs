using FPTBook.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FPTBook.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderedBook> OrderedBooks { get; set; }
    public virtual DbSet<CategoryRequest> CategoryRequests { get; set; }
}
