using interview_dotnet.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace interview_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly PersonContext _dbContext;
        public PersonController(PersonContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/person
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
        {
            if (_dbContext.People == null)
            {
                return NotFound();
            }
            return await _dbContext.People.ToListAsync();
        }

        // GET: api/person/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            if (_dbContext.People == null)
            {
                return NotFound();
            }
            var person = await _dbContext.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return person;
        }

        // POST: api/person
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            _dbContext.People.Add(person);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, person);
        }

        // POST: api/person/readfile
        [HttpPost("readfile")]
        public async Task<ActionResult<List<Person>>> ReadFile(string? filePath)
        {
            List<Person> response = new List<Person>();
            using (var reader = new StreamReader(filePath))
            { 
                while (!reader.EndOfStream)
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        System.Console.WriteLine(line);
                        if (!line.Contains("FirstName"))
                        {
                            var values = line.Split(',');
                            response.Add(new Person(values[0], values[1], values[2]));
                        }
                    }; 
                }
            }
            return response;
        }
    }
}
