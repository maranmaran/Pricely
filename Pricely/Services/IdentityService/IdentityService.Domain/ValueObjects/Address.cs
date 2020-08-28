using System;
using System.Collections.Generic;
using IdentityService.Domain.ValueObjects;

namespace IdentityService.Domain.Entities
{
    public class Address : ValueObjectBase
    {
        public string Country { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}