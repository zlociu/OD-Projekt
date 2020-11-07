using OD_authorize_authenticate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OD_authorize_authenticate.Data
{
    public static class UserInfo
    {
        private static IEnumerable<KeyValuePair<string, List<Claim>>> list = new List<KeyValuePair<string, List<Claim>>>
        {
            new KeyValuePair<string, List<Claim>>("Marcin",
               new List<Claim>
               {
                    new Claim(ClaimTypes.Name, "Marcin"),
                    new Claim(ClaimTypes.Role, "User")
               }),
            new KeyValuePair<string, List<Claim>>("Administrator",
                new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Marcin"),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(ClaimTypes.Role, "Admin")
                })
        };

        public static Dictionary<string, List<Claim>> users = new Dictionary<string, List<Claim>>(collection: list);
    }      
}
