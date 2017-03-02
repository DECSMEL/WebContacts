using IDAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class PhoneVM
    {
        public int PhoneId { get; set; }
        [DisplayName("Phone type")]
        public PhoneTypeVM Type { get; set; }
        [Required]
        [Phone]
        [DisplayName("Phone Number")]
        public string Number { get; set; }
        public int PersonId { get; set; }        
    }
}
