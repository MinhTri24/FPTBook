using FPTBook.Models;

namespace FPTBook.ViewModels;

public class UsersDetail
{
    public List<ApplicationUser> Users { get; set; }
    public string SearchString { get; set; }
}   