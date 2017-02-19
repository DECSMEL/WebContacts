using IDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IQuicklistRepo
    {
        Quicklist GetQuicklist(int id);
        bool AddToQuicklist(int id, string email);

    }
}
