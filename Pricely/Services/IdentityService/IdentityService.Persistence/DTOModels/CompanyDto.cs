using IdentityService.Domain.Entities;
using Newtonsoft.Json;
using System;

namespace IdentityService.Persistence.DTOModels
{
    public class CompanyDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }

        [JsonIgnore]
        public string LogoPath { get; set; }

        public Address Address { get; set; }
        public ContactDetails ContactDetails { get; set; }
    }
}
