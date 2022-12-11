using System.Collections;
using FPTBook.Models;

namespace FPTBook.ViewModels;

public class UsersDetail
{
    public IEnumerable<ApplicationUser> Users { get; set; }
    public string SearchString { get; set; }

}   