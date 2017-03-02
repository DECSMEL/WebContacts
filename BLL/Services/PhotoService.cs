using BLL.Converters;
using BLL.ResultsModel;
using BLL.ViewModel;
using IDAL;
using IDAL.Entities;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PhotoService
    {
        private static IPhotoDAO photoDao = Unity.Container.Resolve<IPhotoDAO>();

        public static OneResult<PhotoVM> GetPhotoById(int id)
        {
            OneResult<PhotoVM> image = new OneResult<PhotoVM>();
            try
            {
                Photo photo = photoDao.GetById(id);
                if (photo != null && !photo.IsPrivate)
                {
                    image.Data = ConverterPersonToContact.ConvertPhotoToPhotoVM(photo);
                    image.IsOk = true;
                }
                else
                {
                    image.Message = "No photo or private photo";
                }
            }
            catch (Exception)
            {
                //TO DO logging
                image.IsOk = false;
                image.Message = "Fail during download photo";
            }
            return image;
        }
    }
}
