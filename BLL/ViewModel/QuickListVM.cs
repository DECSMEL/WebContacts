using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class QuickListVM
    {
        public QuickListVM()
        {
            FavoritePersons = new HashSet<ContactVM>();
        }

        public int QuicklistId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public ICollection<ContactVM> FavoritePersons { get; set;}
    }
}
