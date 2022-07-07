using Microsoft.AspNetCore.Identity;

namespace Backend.Entities
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        
        public Address Address { get; set; }
        
        
    }
}