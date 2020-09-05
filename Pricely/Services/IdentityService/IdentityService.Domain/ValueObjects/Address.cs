using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityService.Domain.ValueObjects
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

        public string GetAddress()
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(Number))
            {
                sb.Append($"{Number} ");
            }
            if (!string.IsNullOrWhiteSpace(Street))
            {
                sb.Append($"{Street} ");
            }
            if (!string.IsNullOrWhiteSpace(City))
            {
                sb.Append($"{City} ");
            }
            if (!string.IsNullOrWhiteSpace(County))
            {
                sb.Append($"{County} ");
            }
            if (!string.IsNullOrWhiteSpace(Country))
            {
                sb.Append($"{Country} ");
            }

            return sb.ToString().TrimEnd();
        }
    }
}