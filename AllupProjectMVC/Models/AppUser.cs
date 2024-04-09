using Microsoft.AspNetCore.Identity;

namespace AllupProjectMVC.Models;

public class AppUser : IdentityUser
{
    public string FullName { get; set; }
    public string LastName { get;  set; }
}
