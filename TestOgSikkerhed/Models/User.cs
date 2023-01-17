using Microsoft.AspNetCore.Identity;

namespace TestOgSikkerhed.Models
{

    // Our user that the table is created from and what we uses to pass data around
    public class User : IdentityUser<Guid>
    {
        public override Guid Id { get; set; }
        public override string? UserName { get; set; }                 //Commented becuase of inheretance, could also override it
        public override string? Email { get; set; }

        public virtual string? Firstname { get; set; }
        public virtual string? Lastname { get; set; }

        public User() { }

    }   
}
