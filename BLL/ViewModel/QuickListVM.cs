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
            FavoritePersons = new List<ContactVM>();
        }

        public int QuicklistId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public IList<ContactVM> FavoritePersons { get; set;}
    }
}
