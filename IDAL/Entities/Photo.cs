using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL.Entities
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public bool IsPrivate { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public virtual Person Person { get; set; }
    }
}
