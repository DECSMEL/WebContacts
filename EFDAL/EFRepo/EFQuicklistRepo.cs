using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using IDAL;
using IDAL.Entities;

namespace EFDAL.EFRepo
{
    public class EFQuicklistRepo : IQuicklistRepo
    {
        private DBContext context;

        public Quicklist GetQuicklist(int id)
        {
            using (context = new DBContext())
            {
                return context.Quicklists.Find(id);
            }
        }

        public bool AddToQuicklist(int id, string email)
        {
            using (context = new DBContext())
            {
                Person p = new Person { Email = email };
                context.Persons.Attach(p);

                Quicklist qlist = new Quicklist { QuicklistId = id };
                context.Quicklists.Add(qlist);
                context.Quicklists.Attach(qlist);
                qlist.Persons.Add(p);

                context.SaveChanges();
                return true;
            }
        }

       
    }
}
