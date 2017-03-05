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
            QuickLists.Add(new QuickListVM() { Name = "Frends" });
        }


        public int PersonId { get; set; }

        [Required(ErrorMessageResourceName = "EmailNeed",
                  ErrorMessageResourceType = typeof(Resource))]
        [StringLength(100)]
        [EmailAddress(ErrorMessageResourceName = "EmailReq",
                      ErrorMessageResourceType = typeof(Resource))]
        [Remote("VerifyEmail","Contact")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "PasswordNeed",
                  ErrorMessageResourceType = typeof(Resource))]
        [StringLength(50)]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$",
                      ErrorMessageResourceName = "PasswordReq",
                      ErrorMessageResourceType = typeof(Resource))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Password",
                  ErrorMessageResourceName = "PasswordNotMatch",
                  ErrorMessageResourceType = typeof(Resource))]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        public string PasswordValidation { get; set; }

        [DisplayName("First name"), StringLength(50)]
        public string FirstName { get; set; }
        [DisplayName("Last name"), StringLength(50)]
        public string LastName { get; set; }
        public PhotoVM Photo { get; set; }

        [DisplayName("Hide photo ?")]
        public bool IsPrivatePhoto { get; set; }
        public BirthDayVM BirthDay { get; set; }

        [DisplayName("Hide birthday ?")]
        public bool IsPrivateBirthDay { get; set; }
        public IList<PhoneVM> Phones { get; set; }
        public IList<QuickListVM> QuickLists { get; set; }

    }
}
