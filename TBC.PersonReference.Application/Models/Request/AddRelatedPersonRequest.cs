using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBC.PersonReference.Core;

namespace TBC.PersonReference.Application.Models.Request
{
    public class AddRelatedPersonRequest
    {
        public int RelatedPersonId { get; set; }
        public RelationType RelationType { get; set; }
    }
}
