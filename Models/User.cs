using Microsoft.AspNetCore.Identity;

namespace ST10028058_CLDV6212_POE_Final.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
