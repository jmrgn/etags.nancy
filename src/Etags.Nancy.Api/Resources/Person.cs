using System;
using Nancy;
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
            this.LastModifiedDate = person.LastModifiedDate;
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

        [JsonProperty("lastModifiedDate")]
        public DateTime LastModifiedDate { get; set; }
    }
    public static class PersonExtension
    {
        public static Models.Person ToModel(this Resources.Person person)
        {
            var model = new Models.Person
            {
                FamilyName = person.FamilyName,
                GivenName = person.GivenName,
                HonorificPrefix = person.HonorificPrefix,
                HonorificSuffix = person.HonorificSuffix,
                Id = person.Id,
                LastModifiedDate = person.LastModifiedDate
            };

            return model;
        }

        public static Models.Person ToModel(this Resources.Person person, Models.Person model)
        {
            model.FamilyName = person.FamilyName ?? model.FamilyName;
            model.GivenName = person.GivenName?? model.GivenName;
            model.HonorificPrefix = person.HonorificPrefix ?? model.HonorificPrefix;
            model.HonorificSuffix = person.HonorificSuffix ?? model.HonorificSuffix;

            return model;
        }
    }
}


