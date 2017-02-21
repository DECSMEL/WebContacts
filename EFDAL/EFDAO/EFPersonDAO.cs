using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using IDAL;
using IDAL.Entities;

namespace EFDAL.EFDAO
{
    public class EFRersonDAO : IPersonDAO
    {

        public IEnumerable<Person> GetAll()
        {
            using (var context = new DBContext())
            {
                var persons = context.Persons.Include(i => i.Phones);
                return persons.ToList();
            }
        }

        public Person GetById(int id)
        {
            using (var context = new DBContext())
            {
                Person person = context.Persons.Include(m => m.Phones)
                                               .Include(m => m.Photo)
                                               .Include(m => m.Quicklist)
                                               .SingleOrDefault(m => m.PersonId == id);
                return person;
            }
        }

        public IEnumerable<Person> Find(string lName)
        {
            using (var context = new DBContext())
            {
                var persons = context.Persons.Where(m => m.LastName.Contains(lName));
                return persons.ToList();
            }
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
                foreach (Phone ph in person.Phones)
                {
                    context.Entry(ph).State = EntityState.Modified;
                }
                foreach (Quicklist ql in person.Quicklist)
                {
                    context.Entry(ql).State = EntityState.Modified;
                }
                //var oldPerson = context.Persons.Find(person.PersonId);
                //oldPerson.Phones = person.Phones;
                //oldPerson.Photo = person.Photo;
                //oldPerson.Quicklist = person.Quicklist;
                context.SaveChanges();
            }
            return true;
        }

        public bool Delete(int id)
        {
            using (var context = new DBContext())
            {
                Person person = context.Persons.Find(id);
                                                            
 //                   .Include(p => p.Photo)                                               .Single(p => p.PersonId == id);

                context.Quicklists.RemoveRange(person.Quicklist);
//                context.Phones.RemoveRange(person.Phones);
                context.Persons.Remove(person);
                context.SaveChanges();
            }
            return true;
        }
    }
                
}
