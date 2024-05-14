using Microsoft.EntityFrameworkCore;
using PeopleManager.Ui.Mvc.Controllers;
using PeopleManager.Ui.Mvc.Models;

namespace PeopleManager.Ui.Mvc.Core
{
    public class PeopleManagerDbContext: DbContext
    {
        public PeopleManagerDbContext(DbContextOptions<PeopleManagerDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Organization> Organizations => Set<Organization>();
        //public IList<Person> People { get; set; } = new List<Person>();
        public DbSet<Person> People => Set<Person>(); // a new DbSet for each new Model

        public void Seed()
        {
            
            Organizations.AddRange(new List<Organization>
            {
                
                new Organization
                {
                    Name = "Amnesty International",
                    Description = "Global movement of people fighting injustice and promoting human rights."
                },
                new Organization
                {
                    Name = "Red Cross",
                    Description =
                        "Humanitarian organization that provides emergency assistance, disaster relief, and education."
                },
                new Organization
                {
                    Name = "World Wildlife Fund",
                    Description =
                        "International non-governmental organization working on issues regarding the conservation, research and restoration of the environment."
                },
                new Organization
                {
                    Name = "UNESCO",
                    Description =
                        "United Nations agency aimed at promoting world peace and security through international cooperation in education, the sciences, and culture."
                },
                new Organization
                {
                    Name = "Doctors Without Borders",
                    Description =
                        "International humanitarian medical non-governmental organisation of French origin best known for its projects in conflict zones and in countries affected by endemic diseases."
                },
                new Organization
                {
                    Name = "Oxfam",
                    Description =
                        "Confederation of 20 independent charitable organizations focusing on the alleviation of global poverty."
                },
                new Organization
                {
                    Name = "Save the Children",
                    Description =
                        "International non-governmental organization that promotes children's rights, provides relief and helps support children in developing countries."
                }

            });

            var unOrganization = new Organization
            {
                Name = "United Nations",
                Description =
                        "International organization founded in 1945 to promote peace, security, and cooperation."
            };
            Organizations.Add(unOrganization);

            var whoOrganization = new Organization
            {
                Name = "World Health Organization",
                Description =
                    "Specialized agency of the United Nations responsible for international public health."
            };
            Organizations.Add(whoOrganization);

            var greenpeaceOrganization = new Organization
            {
                Name = "Greenpeace",
                Description = "Non-governmental environmental organization with offices in over 39 countries."
            };
            Organizations.Add(greenpeaceOrganization);

            //People = new List<Person>
            People.AddRange(new List<Person>
            {
                new Person { FirstName = "John", LastName = "Doe", Email = "johndoe@example.com", Organization = unOrganization },
                new Person { FirstName = "Jane", LastName = "Smith" }, // Email not specified
                new Person { FirstName = "Michael", LastName = "Johnson", Email = "michaeljohnson@example.com", Organization = whoOrganization },
                new Person { FirstName = "Emily", LastName = "Jones" }, // Email not specified
                new Person { FirstName = "Chris", LastName = "Brown", Email = "chrisbrown@example.com" },
                new Person { FirstName = "David", LastName = "Wilson", Organization = whoOrganization }, // Email not specified
                new Person { FirstName = "Olivia", LastName = "Taylor", Email = "oliviataylor@example.com", Organization = greenpeaceOrganization },
                new Person { FirstName = "Daniel", LastName = "Moore" }, // Email not specified
                new Person { FirstName = "Amanda", LastName = "Anderson", Email = "amandaanderson@example.com", Organization = unOrganization },
                new Person { FirstName = "James", LastName = "Thomas", Email = "jamesthomas@example.com" }
            });

            SaveChanges();
        }
    }
}
