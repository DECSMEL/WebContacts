using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.Entities
{
    public class Phone
    { 
        public int PhoneId { get; set; }
        public PhoneType Type { get; set; }
        public string Number { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
