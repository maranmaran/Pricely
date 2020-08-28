using System;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Domain.Entities
{
    public class Company : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string LogoPath { get; set; }

        public Address Address { get; set; }
    }
}
