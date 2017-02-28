using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BLL.ViewModel
{
    public class ContactEditM
    {
        public ContactEditM()
        {
            Phones = new List<PhoneVM>();
            QuickLists = new List<QuickListVM>();
        }


        public int PersonId { get; set; }

        [Required, StringLength(100)]
        [EmailAddress(ErrorMessageResourceName = "EmailReq",
                      ErrorMessageResourceType = typeof(Resource))]
        [Remote("VerifyEmail","Contact")]
        public string Email { get; set; }

        [Required, StringLength(50)]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$",
                      ErrorMessageResourceName = "PasswordReq",
                      ErrorMessageResourceType = typeof(Resource))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Password")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        public string PasswordValidation { get; set; }

        [DisplayName("First name"), StringLength(50)]
        public string FirstName { get; set; }
        [DisplayName("Last name"), StringLength(50)]
        public string LastName { get; set; }
        public PhotoVM Photo { get; set; }
        public IList<PhoneVM> Phones { get; set; }
        public IList<QuickListVM> QuickLists { get; set; }

    }
}
