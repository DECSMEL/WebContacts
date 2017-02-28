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
        IEnumerable<Person> GetAll();
        Person GetById(int id);
        IEnumerable<Person> FindByLastName(string lastName);
        Person GetByEmail(string email);
        bool Create(Person person);
        bool Update(Person person);
        bool Delete(int id);
        bool PasswordCheck(string email, string password);
        bool EmailIsUsed(string email);
    }
}
