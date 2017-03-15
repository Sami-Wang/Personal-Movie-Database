using IdentityServer4.Models;
using IdentityServer4.Validation;
using IdentityServerDBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try {
                var usersFromDB = await IdentityServerDBHelper.ValidateUserNameAndPassword(
                    Startup.Configuration.GetSection("ConnectionString").Value,
                    context.UserName, context.Password);
                if (usersFromDB != null && usersFromDB.Count != 0)
                {
                    context.Result = new GrantValidationResult(
                        usersFromDB[0].userSubject,
                        authenticationMethod: "password");
                }
                else
                {
                    context.Result = new GrantValidationResult(
                        TokenRequestErrors.InvalidGrant,
                        "Invalid User Credential");
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
                context.Result = new GrantValidationResult(
                        TokenRequestErrors.InvalidGrant,
                        "Invalid User Credential");
            }
        }
    }
}
