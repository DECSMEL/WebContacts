using IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.Entities;

namespace EFDAL
{
    public class PhotoDAO : IPhotoDAO
    {
        public Photo GetById(int id)
        {
            using (var context = new DBContext())
            {
                Photo photo = context.Photos.SingleOrDefault(m => m.PhotoId == id);
                return photo;
            }
        }
    }
}
