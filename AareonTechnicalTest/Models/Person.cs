using AareonTechnicalTest.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("AareonTechnicalTest.Tests")]

namespace AareonTechnicalTest.Models
{
    public class Person : IModelBase
    {
        [Key]
        public int Id { get; internal set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public bool IsAdmin { get; set; }
    }
}
