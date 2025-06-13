using Microsoft.AspNetCore.Identity;

namespace RestAPI.Models.Entity
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
