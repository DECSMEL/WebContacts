using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.Entities
{
    public class BirthDay
    {
        public int BirthDayId { get; set; }
        public DateTime Date { get; set; }
        public virtual Person Person { get; set; }

    }
}
