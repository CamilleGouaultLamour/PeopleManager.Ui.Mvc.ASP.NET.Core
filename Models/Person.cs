using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PeopleManager.Ui.Mvc.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("First name")]
        public required string FirstName { get; set; } // NOT NULL

        [Required]
        [DisplayName("Last name")]
        public required string LastName { get; set; } // NOT NULL

        [DisplayName("Email address")]
        [EmailAddress]
        public string? Email { get; set; } // NULL ALLOWED
    
        public int? OrganizationId {  get; set; } // NULL ALLOWED
        //public Organization Organization { get; set; } = null!; // ONLY TIME WE ARE ALLOWED TO USE "null!" !!!!
        public Organization? Organization { get; set; }
    }
}
