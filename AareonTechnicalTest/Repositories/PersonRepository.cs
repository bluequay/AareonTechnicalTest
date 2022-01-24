using AareonTechnicalTest.Interfaces;
using AareonTechnicalTest.Models;

namespace AareonTechnicalTest.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(ApplicationContext context) : base(context)
        { }
    }
}
