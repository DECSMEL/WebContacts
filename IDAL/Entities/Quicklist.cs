using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.Entities
{
    public class Quicklist
    {
        public Quicklist()
        {
            Persons = new List<Person>();
        }

        public int QuicklistId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Person> Persons { get; set; }
    }
}
