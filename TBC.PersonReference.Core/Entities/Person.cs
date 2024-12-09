namespace TBC.PersonReference.Core.Entities
{
    public class Person : EntityBase
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public Sex Sex { get; set; }
        public string PrivateNumber { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string? City { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
        public string? Image { get; set; }
        public ICollection<PersonRelation> RelatedPersons { get; set; } = new List<PersonRelation>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
