using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using IDAL;
using IDAL.Entities;

namespace EFDAL
{
    public class EFQuicklistDAO : IQuicklistDAO
    {
        private DBContext context;

        public Quicklist GetQuicklist(int id)
        {
            using (context = new DBContext())
            {
                return context.Quicklists.Find(id);
            }
        }

        public bool AddToQuicklist(int listId, int personId)
        {
            using (context = new DBContext())
            {
                Person person = new Person { PersonId = personId };
                context.Persons.Attach(person);

                Quicklist qlist = new Quicklist { QuicklistId = listId };
                context.Quicklists.Add(qlist);
                context.Quicklists.Attach(qlist);
                qlist.Persons.Add(person);

                context.SaveChanges();
                return true;
            }
        }

       
    }
}
