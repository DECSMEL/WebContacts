using IDAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class PhoneVM
    {
        public int PhoneId { get; set; }
        public PhoneType Type { get; set; }
        [StringLength(50)]
        [RegularExpression("^\\+[1-9]{1}[0-9]{3,20}$")]
        public string Number { get; set; }
        public int PersonId { get; set; }

        
    }
}
