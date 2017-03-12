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
    public class RersonDAO : IPersonDAO
    {
        public IList<Person> GetAll()
        {
            using (var context = new DBContext())
            {
                var persons = context.Persons.Include(i => i.Phones).OrderBy(l => l.LastName);
                if (persons == null) return null;
                else return persons.ToList();
            }
        }
        public IList<Person> FindByLastName(string lastName)
        {
            using (var context = new DBContext())
            {
                var persons = context.Persons.Include(p => p.Phones)
                                     .Where(m => m.LastName.Contains(lastName));
                return persons.ToList();
            }
        }
        public Person GetById(int id)
        {
            using (var context = new DBContext())
            {
                return context.Persons.Include(m => m.Phones)
                                      .Include(m => m.Photo)
                                      .Include(b => b.BirthDay)
                                      .SingleOrDefault(m => m.PersonId == id);
            }
        }
        public Person GetByEmail(string email)
        {
            using (var context = new DBContext())
            {
                return context.Persons.Single(m => m.Email == email);
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
        public bool Edit(Person person, bool withPhoto)
        {
            using (var context = new DBContext())
            {
                
                if (person.BirthDay != null && person.BirthDay.BirthDayId == 0)
                {
                    person.BirthDay.BirthDayId = person.PersonId;
                    context.Days.Add(person.BirthDay);
                }

                var phones = context.Phones.Where(p => p.PersonId == person.PersonId);
                context.Phones.RemoveRange(phones);
                context.Phones.AddRange(person.Phones);

                context.Persons.Attach(person);
                var entry = context.Entry(person);
                entry.State = EntityState.Modified;
                entry.Property(e => e.Email).IsModified = false;
                entry.Property(e => e.Password).IsModified = false;

                if (withPhoto)
                {
                    context.Entry(person.Photo).State = EntityState.Modified;
                }
                else
                {
                    context.Entry(person.Photo).State = EntityState.Unchanged;
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
        public bool PasswordCheck(string email, string password)
        {
            bool userValid = false;
            using (var context = new DBContext())
            {
                userValid = context.Persons.Any(p => p.Email == email && p.Password == password);
            }
            return userValid;
        }
        public bool PasswordCheck(int id, string password)
        {
            bool userValid = false;
            using (var context = new DBContext())
            {
                userValid = context.Persons.Any(p => p.PersonId == id && p.Password == password);
            }
            return userValid;
        }
        public bool EmailIsUsed(string email)
        {
            bool emailIsUsed = true;
            using (var context = new DBContext())
            {
                emailIsUsed = context.Persons.Any(p => p.Email == email);
            }
            return emailIsUsed;
        }
    }
                
}
