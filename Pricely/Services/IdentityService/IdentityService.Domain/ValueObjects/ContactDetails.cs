using System;
using System.Collections.Generic;

namespace IdentityService.Domain.ValueObjects
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