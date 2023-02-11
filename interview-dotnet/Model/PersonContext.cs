using Microsoft.EntityFrameworkCore;

namespace interview_dotnet.Model
{
    public class PersonContext: DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options)
            : base(options)
        { 
        }

        public DbSet<Person> People { get; set; } = null!;
    }
}
