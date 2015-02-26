using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Etags.Nancy.Api.Models;
using FluentNHibernate.Mapping;

namespace Etags.Nancy.Api.Maps
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Id(a => a.Id);
            Map(a => a.FamilyName);
            Map(a => a.GivenName);
            Map(a => a.HonorificPrefix);
            Map(a => a.HonorificSuffix);
            Map(a => a.LastModifiedDate);
        }
    }
}
