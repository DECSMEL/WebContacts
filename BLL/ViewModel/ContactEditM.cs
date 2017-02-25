using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class ContactEditM
    {
        public int PersonId { get; set; }
        [Required, StringLength(100)]
        [RegularExpression("^([\\w\\.\\-] +)@([\\w\\-] +)((\\.(\\w){2, 3})+)$")]
        public string Email { get; set; }
        [Required, StringLength(50)]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$")]
        public string Password { get; set; }
        [DisplayName("First name"), StringLength(50)]
        public string FirstName { get; set; }
        [DisplayName("Last name"), StringLength(50)]
        public string LastName { get; set; }
        public PhotoVM Photo { get; set; }
        public IList<PhoneVM> Phones { get; set; }
        public IList<QuickListVM> QuickLists { get; set; }

    }
}
