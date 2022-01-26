using AareonTechnicalTest.Models;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Interfaces
{
    public interface IPersonService
    {
        ValueTask<Person> GetAsync(int id);
    }
}
