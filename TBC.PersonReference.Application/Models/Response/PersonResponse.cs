using TBC.PersonReference.Core;
using TBC.PersonReference.Core.Entities;

namespace TBC.PersonReference.Application.Models.Response
{
    public class PersonResponse
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public Sex Sex { get; set; }
        public string PrivateNumber { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string? City { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
        public string? Image { get; set; }
        public ICollection<int> RelatedPersons { get; set; } = new List<int>();
    }
}
