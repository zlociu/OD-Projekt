using OD_authorize_authenticate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

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
                    new Claim(ClaimTypes.Name, "Administrator"),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(ClaimTypes.Role, "Admin")
                })
        };

        private static IEnumerable<KeyValuePair<string, string>> listPass = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("Marcin", Encoding.ASCII.GetString(SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes("1qa2ws3ed")))),
            new KeyValuePair<string, string>("Administrator", Encoding.ASCII.GetString(SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes("1qa2ws3ed")))),
        };

        public static Dictionary<string, List<Claim>> users = new Dictionary<string, List<Claim>>(collection: list);
        public static Dictionary<string, string> usersPasswords = new Dictionary<string, string>(collection: listPass);
    }      
}
