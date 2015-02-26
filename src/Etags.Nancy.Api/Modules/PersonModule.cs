
using System;
using System.Linq;
using System.Threading.Tasks;
using Etags.Nancy.Api.Cache;
using Etags.Nancy.Api.Resources;
using Etags.Nancy.Api.Persistence;
using Nancy;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Etags.Nancy.Api.Modules
{
    public class PersonModule : NancyModule
    {
        private readonly IPersonRepository repository;
        private readonly ICache cache;

        public PersonModule(IPersonRepository repository, ICache cache)
        {
            this.repository = repository;
            this.cache = cache;
            Get["/persons"] = GetPersons;
            Get["/persons/{id}"] = GetPerson;
            Post["/persons"] = AddPerson;
            Put["/persons/{id}"] = EditPerson;
            OnError += (ctx, ex) =>
            {
                return ex.Message;
            };
            // http://stackoverflow.com/questions/12726113/does-nancyfx-support-static-content-caching-via-the-etag-and-last-modified-heade
            // http://simoncropp.com/conditionalresponseswithnancyfx
            // https://github.com/NancyFx/Nancy.Bootstrappers.Windsor/blob/master/src/Nancy.BootStrappers.Windsor/WindsorNancyBootstrapper.cs
        }


        private Tuple<string, string>[] GetHeaders(Person person)
        {
            var headers = new Tuple<string, string>[2];
            var tag = string.Format("{0}-{1}", person.Id, person.LastModifiedDate.Ticks);
            headers[0] = new Tuple<string, string>("Etag", tag);
            headers[1] = new Tuple<string, string>( "Last-Modified", person.LastModifiedDate.ToString());
            return headers;
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
            var result = new Resources.Person(person);
           
            return Negotiate.WithModel(result)
                            .WithStatusCode(HttpStatusCode.OK)
                            .WithHeaders(GetHeaders(result));
        }

        private dynamic AddPerson(dynamic req)
        {
            var serializer = new JsonSerializer();
            var person = serializer.Deserialize<Resources.Person>(req);
            var personModel = person.ToModel();
            repository.Add(personModel);
            var result = new Person(personModel);

            return Negotiate.WithModel(result)
                            .WithStatusCode(HttpStatusCode.OK)
                            .WithHeaders(GetHeaders(result));
        }

        private dynamic EditPerson(dynamic req)
        {
            var serializer = new JsonSerializer();
            var person = serializer.Deserialize<Resources.Person>(req);
            var old = repository.Get(req.Id);

            var personModel = person.ToModel();
            repository.Edit(personModel);
            var result = new Person(personModel);
            return Negotiate.WithModel(result)
                            .WithStatusCode(HttpStatusCode.OK)
                            .WithHeaders(GetHeaders(result));
        }
    }
}
