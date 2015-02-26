using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etags.Nancy.Api.Models;
using NHibernate.Linq.ReWriters;

namespace Etags.Nancy.Api.Persistence
{
    public interface IPersonRepository
    {
        Person Get(int id);
        IEnumerable<Person> GetAll();
        void Add(Person person);
        void Edit(Person person);
    }
}
