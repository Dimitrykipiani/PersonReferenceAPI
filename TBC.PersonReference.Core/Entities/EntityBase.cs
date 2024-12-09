using System.ComponentModel.DataAnnotations;

namespace TBC.PersonReference.Core.Entities
{
    public class EntityBase
    {
        [Required]
        public int Id { get; set; }
    }
}
