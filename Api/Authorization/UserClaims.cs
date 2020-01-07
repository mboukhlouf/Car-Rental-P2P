using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace Api.Authorization
{
    public class UserClaims
    {
        public Dictionary<String, Claim> Claims { get; } = new Dictionary<string, Claim>();

        public int Id
        {
            get => int.Parse(Claims["id"].Value);
            set => Claims["id"] = new Claim("id", value.ToString(), ClaimValueTypes.Integer);
        }

        public String Username
        {
            get => Claims["username"].Value;
            set => Claims["username"] = new Claim("username", value, ClaimValueTypes.String);
        }

        public bool IsAdmin
        {
            get => bool.Parse(Claims["isAdmin"].Value);
            set => Claims["isAdmin"] = new Claim("isAdmin", value.ToString(), ClaimValueTypes.Boolean);
        }

        public UserClaims()
        {
        }

        public UserClaims(int id, String username, bool isAdmin)
        {
            Id = id;
            Username = username;
            IsAdmin = isAdmin;
        }

        public static UserClaims FromClaimsPrincipal(ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
            {
                return null;
            }
            
            int id = int.Parse(user.Claims.FirstOrDefault(claim => claim.Type == "id").Value);
            String username = user.Claims.FirstOrDefault(claim => claim.Type == "username").Value;
            bool isAdmin = bool.Parse(user.Claims.FirstOrDefault(claim => claim.Type == "isAdmin").Value);

            return new UserClaims(id, username, isAdmin);
        }
    }
}
