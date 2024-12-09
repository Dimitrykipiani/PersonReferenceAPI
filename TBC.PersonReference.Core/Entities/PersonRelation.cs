namespace TBC.PersonReference.Core.Entities
{
    public class PersonRelation : EntityBase
    {
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int RelatedPersonId { get; set; }
        public Person RelatedPerson { get; set; }
        public RelationType RelationType { get; set; }
    }
}
