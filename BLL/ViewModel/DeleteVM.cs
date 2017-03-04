using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class DeleteVM
    {
        [Required(ErrorMessageResourceName = "PasswordNeed",
                 ErrorMessageResourceType = typeof(Resource))]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
