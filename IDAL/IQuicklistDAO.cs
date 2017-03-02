using IDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IQuicklistDAO
    {
        Quicklist GetQuicklist(int id);
        bool AddToQuicklist(int listId, int personId);
        bool RemoveFromQuicklist(int listId, int personId);

    }
}
