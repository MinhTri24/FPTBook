using FPTBook.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
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
    public virtual DbSet<OrderOrderedBook> OrderOrderedBooks { get; set; }
    public virtual DbSet<CategoryRequest> CategoryRequests { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderOrderedBook>().HasKey(oob => new { oob.OrderId, oob.OrderedBookId});
        base.OnModelCreating(modelBuilder);
    }
}
