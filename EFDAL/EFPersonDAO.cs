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
    public class EFRersonDAO : IPersonDAO
    {

        public IEnumerable<Person> GetAll()
        {
            using (var context = new DBContext())
            {
                var persons = context.Persons.Include(i => i.Phones).OrderBy(l => l.LastName);
                if (persons == null) return null;
                else return persons.ToList();
            }
        }

        public Person GetById(int id)
        {
            using (var context = new DBContext())
            {
                Person person = context.Persons.Include(m => m.Phones)
                                               .Include(m => m.Photo)
                                               .SingleOrDefault(m => m.PersonId == id);
                return person;
            }
        }

        public IEnumerable<Person> Find(string lastName)
        {
            using (var context = new DBContext())
            {
                var persons = context.Persons.Where(m => m.LastName.Contains(lastName));
                return persons.ToList();
            }
        }

        public bool PasswordCheck(string email, string password)
        {
            bool userValid = false;
            using (var context = new DBContext())
            {
                userValid  = context.Persons.Any(p => p.Email == email && p.Password == password);
            }
            return userValid;
        }



        public bool Create(Person person)
        {
            using (var context = new DBContext())
            {
                context.Persons.Add(person);
                context.SaveChanges();
            }
            return true;
        }

        public bool Update(Person person)
        {
            using (var context = new DBContext())
            {
                context.Persons.Attach(person);
                context.Entry(person).State = EntityState.Modified;
                context.Entry(person.Photo).State = EntityState.Modified;
                foreach (Phone phone in person.Phones)
                {
                    if (phone.PhoneId != 0)
                    {
                        context.Entry(phone).State = EntityState.Modified;
                    }
                    else
                    {
                        context.Phones.Add(phone);
                    }
                }
                foreach (Quicklist ql in person.Quicklist)
                {
                    if (ql.QuicklistId != 0)
                    {
                        context.Entry(ql).State = EntityState.Modified;
                    }
                    else
                    {
                        context.Quicklists.Add(ql);
                    }
                }
                context.SaveChanges();
            }
            return true;
        }

        public bool Delete(int id)
        {
            using (var context = new DBContext())
            {
                Person person = context.Persons.Find(id);                                                           
                context.Quicklists.RemoveRange(person.Quicklist);
                context.Persons.Remove(person);
                context.SaveChanges();
            }
            return true;
        }
    }
                
}
