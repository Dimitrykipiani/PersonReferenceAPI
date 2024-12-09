namespace TBC.PersonReference.Core.Entities
{
    public class PhoneNumber : EntityBase
    {
        public NumberType NumberType { get; set; }
        public string Number { get; set; } = string.Empty;
    }
}
