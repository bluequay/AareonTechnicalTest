using AareonTechnicalTest.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace AareonTechnicalTest.Models
{
    public class Note : IModelBase
    {
        [Key]
        public int Id { get; internal set; }

        public int TicketId { get; set; }

        public int PersonId { get; set; }

        public string Content { get; set; }
    }
}
