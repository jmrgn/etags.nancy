using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etags.Nancy.Api.Models
{
    public class Person 
    {
        public virtual int Id { get; set; }
        public virtual string GivenName { get; set; }
        public virtual string FamilyName { get; set; }
        public virtual string HonorificPrefix { get; set; }
        public virtual string HonorificSuffix { get; set; }
        public virtual DateTime LastModifiedDate { get; set; }
    }
}
