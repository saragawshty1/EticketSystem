using Microsoft.AspNetCore.Identity;

namespace MOVIES.Models
{
    public class ApplicationUser : IdentityUser
    {
        public  string  Address {get;set;}
    }
}
