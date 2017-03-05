using IDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IPersonDAO
    {
        IList<Person> GetAll();
        IList<Person> FindByLastName(string lastName);
        Person GetById(int id);
        Person GetByEmail(string email);
        bool Create(Person person);
        bool Edit(Person person, bool withPhoto);
        bool Delete(int id);
        bool PasswordCheck(string email, string password);
        bool PasswordCheck(int id, string password);
        bool EmailIsUsed(string email);
    }
}
