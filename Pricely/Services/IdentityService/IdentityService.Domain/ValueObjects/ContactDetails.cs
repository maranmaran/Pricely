using System;
using System.Collections.Generic;
using IdentityService.Domain.ValueObjects;

namespace IdentityService.Domain.Entities
{
    public class ContactDetails: ValueObjectBase
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}