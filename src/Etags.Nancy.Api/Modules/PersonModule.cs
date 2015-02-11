
using System;
using System.Linq;
using Etags.Nancy.Api.Resources;
using Etags.Nancy.Api.Persistence;
using Nancy;

namespace Etags.Nancy.Api.Modules
{
    public class PersonModule : NancyModule
    {
        private readonly IPersonRepository repository;

        public PersonModule(IPersonRepository repository)
        {
            this.repository = repository;
            Get["/persons"] = GetPersons;
            Get["/persons/{id}"] = GetPerson;
        }

        private dynamic GetPersons(dynamic arg)
        {
            var products = repository.GetAll()
                .Select(p => new Person(p))
                .ToList();

            return Response.AsJson(products);
        }

        private dynamic GetPerson(dynamic req)
        {
            var id = req.id;
            var person = repository.Get(id );
            Person result = new Person(person);
            return Response.AsJson(result);
        }
    }
}
