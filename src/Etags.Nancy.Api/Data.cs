using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etags.Nancy.Api.Models;
using Etags.Nancy.Api.Persistence;

namespace Etags.Nancy.Api
{
    public static class Data
    {
        public static void Create()
        {
            var personRepository = new PersonRepository();

            var person1 = new Person
            {
                FamilyName = "Morgan",
                GivenName = "James",
                HonorificPrefix = "His Majesty",
                HonorificSuffix = "The Great",
                Id = 1
            };

            var person2 = new Person
            {
                FamilyName = "Doe",
                GivenName = "John",
                HonorificPrefix = "Mr.",
                Id = 2 
            };

            var person3 = new Person
            {
                FamilyName = "Doe",
                GivenName = "Jane",
                HonorificPrefix = "Ms.",
                Id = 3
            };

            var person4 = new Person
            {
                FamilyName = "Else",
                GivenName = "Someone",
                HonorificSuffix = "III",
                Id = 4 
            };

            var person5 = new Person
            {
                FamilyName = "Bla",
                GivenName = "Blargh",
                HonorificSuffix = "Jr.",

            };

           // personRepository.Add(person1);
           // personRepository.Add(person2);
           //  personRepository.Add(person3);
           //  personRepository.Add(person4);
            personRepository.Add(person5);
        }
    }
}
