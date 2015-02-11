using Newtonsoft.Json;
using Models = Etags.Nancy.Api.Models;

namespace Etags.Nancy.Api.Resources
{
    public class Person  
    {
        public Person()
        {
            
        }

        public Person(Models.Person person)
        {
            this.Id = person.Id;
            this.FamilyName = person.FamilyName;
            this.GivenName = person.GivenName;
            this.HonorificPrefix = person.HonorificPrefix;
            this.HonorificSuffix = person.HonorificSuffix;
        }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("givenName")]
        public string GivenName { get; set; }

        [JsonProperty("familyName")]
        public string FamilyName { get; set; }

        [JsonProperty("honorificPrefix")]
        public string HonorificPrefix { get; set; }

        [JsonProperty("honorificSuffix")]
        public string HonorificSuffix { get; set; }
    }
}
