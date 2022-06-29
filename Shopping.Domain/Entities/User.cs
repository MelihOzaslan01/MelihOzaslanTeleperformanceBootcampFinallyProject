using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Shopping.Domain.Entities
{
    public class User:IdentityUser,IBaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
