using AareonTechnicalTest.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace AareonTechnicalTest.Models
{
    public class Ticket : IModelBase
    {
        [Key]
        public int Id { get; }

        public string Content { get; set; }

        public int PersonId { get; set; }
    }
}
