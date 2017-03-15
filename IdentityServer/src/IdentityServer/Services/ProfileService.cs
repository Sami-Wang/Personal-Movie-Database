using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using System.Security.Claims;
using IdentityServerDBConnection;

namespace IdentityServer.Services
{
    public class ProfileService : IProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            List<Claim> claimList = new List<Claim>();
            try {
                string userSubject = context.Subject.Claims.ToList().Find(us => us.Type == "sub").Value;
                var rolesFromDB = await IdentityServerDBHelper.GetRolesByUserSubject(
                    Startup.Configuration.GetSection("ConnectionString").Value,
                    userSubject);
                if (rolesFromDB != null && rolesFromDB.Count != 0)
                {
                    for (int i = 0; i < rolesFromDB.Count; i++)
                    {
                        claimList.Add(new Claim("role", rolesFromDB[i].roleName));
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.FromResult(0);
        }
    }
}
