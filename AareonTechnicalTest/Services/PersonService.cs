using AareonTechnicalTest.Interfaces;
using AareonTechnicalTest.Models;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Services
{
    public class PersonService : IPersonService
    {

        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public ValueTask<Person> GetAsync(int id)
        {
            return _personRepository.GetAsync(id);
        }
    }
}
