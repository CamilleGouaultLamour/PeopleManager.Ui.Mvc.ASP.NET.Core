using System.ComponentModel.DataAnnotations;

namespace PeopleManager.Ui.Mvc.Models
{
    public class Organization
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; } // NOT NULL

        public string? Description { get; set; } // NULL ALLOWED
    
        public IList<Person> Members { get; set; } = new List<Person>(); // HERE THE DISTINCTION BETWEEN NULL AND AN EMPTY LIST IS NOT IMPORTANT
    }
}