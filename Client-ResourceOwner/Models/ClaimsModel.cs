using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Client_ResourceOwner.Models
{
    public class ClaimsModel
    {
        public string Header { get; set; }
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public IEnumerable<KeyValuePair<string, string>> Claims { get; set; }

        public ClaimsModel() { }

        public ClaimsModel(IEnumerable<KeyValuePair<string, string>> claims)
        {
            Claims = claims;
        }
    }
}