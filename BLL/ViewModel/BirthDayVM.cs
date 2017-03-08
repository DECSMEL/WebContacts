using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class BirthDayVM
    {
        public int BirthDayId { get; set; }

        [DisplayName("Birthday")]
        [DataType(DataType.Date)]
//        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

    }
}
