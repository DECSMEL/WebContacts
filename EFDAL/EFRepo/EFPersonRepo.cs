using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using IDAL;
using EFDAL.Entities;

namespace EFDAL.EFRepo
{
    public class EFRersonRepo : IRepo<Person>
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

        public Person GetById(int id)
        {
            using (context = new DBContext())
            {
                Person person = context.Persons.Include(m => m.Phones)
                                               .Include(m => m.Photo)
                                               .SingleOrDefault(m => m.PersonId == id);
                return person;
            }
        }

        public IEnumerable<Person> Find(Func<Person, bool> predicate)
        {
            throw new NotImplementedException();
        }



        public void Create(Person person)
        {
            using (context = new DBContext())
            {
                context.Persons.Add(person);
                context.SaveChanges();
            }          
        }

        public void Update(Person person)
        {
            using (context = new DBContext())
            {
                Person oldPerson = context.Persons.Include(i => i.Phones)
                                                  .SingleOrDefault(x => x.PersonId == person.PersonId);
                oldPerson.Phones = person.Phones;
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (context = new DBContext())
            {
                Person person = context.Persons.Include(m => m.Phones)
                                               .Include(m => m.Photo)
                                               .SingleOrDefault(m => m.PersonId == id);                              
                context.Persons.Remove(person);
                context.SaveChanges();
            }
        }







        
    }
}
