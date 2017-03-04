using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModel
{
    public class PhotoVM
    {
        public int PhotoId { get; set; }

        [DisplayName("Private photo ?")]
        public bool IsPrivate { get; set; }
        public byte[] ImageData { get; set; }
        [StringLength(50)]
        public string ImageMimeType { get; set; }
    }
}
