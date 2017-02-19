using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.Entities
{
    public class Person
    {
        public Person()
        {
            Phones = new List<Phone>();
            Quicklists = new List<Quicklist>();
        }

        public int PersonId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
        public virtual Photo Photo { get; set; }
        public virtual ICollection<Quicklist> Quicklists { get; set; }

    }
}
