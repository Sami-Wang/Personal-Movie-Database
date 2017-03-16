using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServerDBConnection;

namespace IdentityServer.Stores
{
    public class ResourceStore : IResourceStore
    {
        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            List<ApiResource> resources = new List<ApiResource>();
            try
            {
                foreach (string sn in scopeNames)
                {
                    var resourcesFromDB = await IdentityServerDBHelper.GetResourceByScopeName(
                        Startup.Configuration.GetSection("ConnectionString").Value, sn);
                    if (resourcesFromDB != null && resourcesFromDB.Count != 0)
                    {
                        if (resources.Where(r => r.Name == resourcesFromDB[0].resourceName).ToList().Count > 0)
                        {
                            resources.Where(r => r.Name == resourcesFromDB[0].resourceName).ToList()[0].Scopes.Add(
                                new Scope()
                                {
                                    Name = resourcesFromDB[0].scopeName,
                                    DisplayName = resourcesFromDB[0].scopeDisplayName,
                                    Description = resourcesFromDB[0].scopeDescription
                                });
                        }
                        else {
                            ApiResource apiResource = new ApiResource();
                            apiResource.Name = resourcesFromDB[0].resourceName;
                            apiResource.Description = resourcesFromDB[0].resourceDescription;
                            string apiSecrets = resourcesFromDB[0].resourceSecrets;
                            apiResource.ApiSecrets = new List<Secret>() {
                                new Secret(apiSecrets.Sha256())
                            };
                            apiResource.Enabled = resourcesFromDB[0].enabled;
                            apiResource.Scopes = new List<Scope>() {
                                new Scope() {
                                    Name = resourcesFromDB[0].scopeName,
                                    DisplayName = resourcesFromDB[0].scopeDisplayName,
                                    Description = resourcesFromDB[0].scopeDescription
                                }
                            };
                            apiResource.UserClaims = new List<string>() {
                                "role"
                            };
                            resources.Add(apiResource);
                        }                        
                    }
                }
                return resources;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return new List<ApiResource>();
            }
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            IEnumerable<IdentityResource> identityResources = new List<IdentityResource>();
            return Task.FromResult<IEnumerable<IdentityResource>>(identityResources);
        }

        public async Task<Resources> GetAllResources()
        {
            List<ApiResource> apiResources = new List<ApiResource>();
            try
            {
                var resourcesFromDB = await IdentityServerDBHelper.GetAllResources(
                        Startup.Configuration.GetSection("ConnectionString").Value);
                if (resourcesFromDB != null && resourcesFromDB.Count != 0)
                {
                    for (int i = 0; i < resourcesFromDB.Count; i++)
                    {
                        if (apiResources.Where(
                            r => r.Name == resourcesFromDB[i].resourceName).ToList().Count > 0)
                        {
                            apiResources.Where(
                                r => r.Name == resourcesFromDB[i].resourceName).ToList()[0].Scopes.Add(
                                new Scope()
                                {
                                    Name = resourcesFromDB[i].scopeName,
                                    DisplayName = resourcesFromDB[i].scopeDisplayName,
                                    Description = resourcesFromDB[i].scopeDescription
                                });
                        }
                        else {
                            ApiResource apiResource = new ApiResource();
                            apiResource.Name = resourcesFromDB[i].resourceName;
                            apiResource.Description = resourcesFromDB[i].resourceDescription;
                            string apiSecrets = resourcesFromDB[i].resourceSecrets;
                            apiResource.ApiSecrets = new List<Secret>() {
                                new Secret(apiSecrets.Sha256()
                            )};
                            apiResource.Enabled = resourcesFromDB[i].enabled;
                            apiResource.Scopes = new List<Scope>() {
                                new Scope() {
                                    Name = resourcesFromDB[i].scopeName,
                                    DisplayName = resourcesFromDB[i].scopeDisplayName,
                                    Description = resourcesFromDB[i].scopeDescription
                                }
                            };
                            apiResource.UserClaims = new List<string>() {
                                "role"
                            };
                            apiResources.Add(apiResource);
                        }                    
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            return new Resources()
            {
                ApiResources = apiResources
            };
        }
    }
}
