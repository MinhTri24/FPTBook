using Microsoft.AspNetCore.Identity;

namespace FPTBook.Models;

public class ApplicationUser : IdentityUser
{
    [PersonalData]
    public string Fullname { get; set; }
    [PersonalData]
    public string HomeAddress { get; set; }
}   