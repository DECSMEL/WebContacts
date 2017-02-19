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
    public class EFRersonRepo : IPersonRepo
    {
        private DBContext context;

        public IEnumerable<Person> GetAll()
        {
            using (context = new DBContext())
            {
                var persons = context.Persons.Include(i => i.Phones);
                return persons.ToList();
            }
        }

        public Person GetById(string email)
        {
            using (context = new DBContext())
            {
                Person person = context.Persons.Include(m => m.Phones)
                                               .Include(m => m.Photo)
                                               .Include(m => m.Quicklist)
                                               .Single(m => m.Email == email);
                return person;
            }
        }

        public IEnumerable<Person> Find(string lName)
        {
            using (context = new DBContext())
            {
                var persons = context.Persons.Where(m => m.LastName.Contains(lName));
                return persons.ToList();
            }
        }



        public bool Create(Person person)
        {
            using (context = new DBContext())
            {
                context.Persons.Add(person);
                context.SaveChanges();
            }
            return true;
        }

        public bool Update(Person person)
        {
            using (context = new DBContext())
            {
                Person oldPerson = context.Persons.Find(person.Email);
                context.Phones.RemoveRange(oldPerson.Phones);
                oldPerson.Phones = person.Phones;
                context.SaveChanges();
            }
            return true;
        }

        public bool Delete(string email)
        {
            using (context = new DBContext())
            {
                Person person = context.Persons.Include(p => p.Photo)
                                               .Single(p => p.Email == email);

                context.Quicklists.RemoveRange(person.Quicklist);
                context.Phones.RemoveRange(person.Phones);
                context.Persons.Remove(person);
                context.SaveChanges();
            }
            return true;
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

        public List<Person> GetQuicklist(int id)
        {
            using (context = new DBContext())
            {
                return context.Quicklists.Find(id).Persons.ToList();
            }

        }
    }
}
