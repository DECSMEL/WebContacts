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
    public class QuicklistDAO : IQuicklistDAO
    {
        public Quicklist GetById(int listId)
        {
            using (var context = new DBContext())
            {
                return context.Quicklists.Include(p => p.Persons.Select(ph => ph.Phones))
                                         .Where(l => l.QuicklistId == listId)
                                         .SingleOrDefault();

            }
        }

        public bool Create(int personId)
        {
            using (var context = new DBContext())
            {
                Quicklist list = new Quicklist() { QuicklistId = personId, Name = "Frends" };
                context.Quicklists.Add(list);
                context.SaveChanges();
            }
            return true;
        }

        public bool AddToQuicklist(int listId, int personId)
        {
            using (var context = new DBContext())
            {
                Person person = new Person { PersonId = personId };
                context.Persons.Attach(person);

                Quicklist qlist = new Quicklist { QuicklistId = listId };
                context.Quicklists.Add(qlist);
                context.Quicklists.Attach(qlist);
                qlist.Persons.Add(person);

                context.SaveChanges();
            }
            return true;
        }

        public bool RemoveFromQuicklist(int listId, int personId)
        {
            using (var context = new DBContext())
            {
                Person person = new Person { PersonId = personId };
                context.Persons.Attach(person);
                var list = context.Quicklists.Find(listId);
                list.Persons.Remove(person);
                context.SaveChanges();
            }
            return true;
        }

        public bool IsInQuicklist(int listId, int personId)
        {
            using (var context = new DBContext())
            {
                var list = context.Quicklists.Include(p => p.Persons)
                                             .Where(l => l.QuicklistId == listId)
                                             .Single();
                foreach (Person p in list.Persons)
                {
                    if (p.PersonId == personId) return true;
                }
            }
            return false;
        }
    }
}