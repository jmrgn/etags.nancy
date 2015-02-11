using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etags.Nancy.Api.Models;
using NHibernate;

namespace Etags.Nancy.Api.Persistence
{
    public class PersonRepository : IPersonRepository
    {
        public PersonRepository()
        {
            
        }
        public Person Get(int id)
        {
            using (ISession session = Global.SessionFactory.OpenSession())
            {
                return session.QueryOver<Person>().Where(p => p.Id == id).SingleOrDefault();
            }
        }

        public IEnumerable<Person> GetAll()
        {
            using (ISession session = Global.SessionFactory.OpenSession())
            {
                return session.QueryOver<Person>()
                    .List();
            }
        }

        public void Add(Person person)
        {
            using (ISession session = Global.SessionFactory.OpenSession())
            using (ITransaction txn = session.BeginTransaction())
            {
                session.Save(person);
                txn.Commit();
            }
        }
    }
}
