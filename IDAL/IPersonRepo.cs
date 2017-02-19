using IDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IPersonRepo
    {
        IEnumerable<Person> GetAll();
        Person GetById(string email);
        IEnumerable<Person> Find(string lastName);
        bool Create(Person person);
        bool Update(Person person);
        bool Delete(string email);
        bool AddToQuicklist(int id, string email);

    }
}
