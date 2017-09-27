using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HonanClaimsPortal.Models.ProtalLogingRequest
{
    public class ProtalLogingRequestModel
    {
        public string keyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public DateTime LastLogin { get; set; }
    }
}