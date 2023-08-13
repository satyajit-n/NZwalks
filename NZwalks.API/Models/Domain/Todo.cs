using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.Domain
{
    public class Todo
    {
        public Guid Id { get; set; }
        public string TodoName { get; set; }
    }
}
