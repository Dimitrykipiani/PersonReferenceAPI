using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBC.PersonReference.Core;

namespace TBC.PersonReference.Application.Models.Response
{
    public class RelatedPersonReportDto
    {
        public RelationType RelationType { get; set; }
        public int Count { get; set; }
    }
}
