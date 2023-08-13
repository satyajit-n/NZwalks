using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        //Navigation Properties
        public Todo Todo { get; set; }
    }
}
