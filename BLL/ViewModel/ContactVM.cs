using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class ContactVM
    {
        public ContactVM(int contactId, string firstName, string lastName, IList<PhoneVM> phones, PhotoVM photo = null)
        {
            this.ContactId = contactId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Photo = photo;
            this.Phones = phones;
        }

        public int ContactId { get; private set; }
        [DisplayName("First name")]
        public string FirstName { get; private set; }
        [DisplayName("Last name")]
        public string LastName { get; private set; }
        public PhotoVM Photo { get; private set; }
        public IList<PhoneVM> Phones { get; private set; }

        public override int GetHashCode()
        {
            return ContactId;
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            ContactVM temp = (ContactVM)obj;
            return this.ContactId == temp.ContactId;
        }

    }
}
