using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OD_authorize_authenticate.Models
{
    public class User
    {
        public string userName { get; set; }
        public List<Claim> claims { get; set; }
    }
}
