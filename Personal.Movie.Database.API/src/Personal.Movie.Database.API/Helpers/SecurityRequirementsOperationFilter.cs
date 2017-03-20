using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;

namespace Personal.Movie.Database.API.Helpers
{
    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var scopes = Startup.Configuration.GetSection("SwaggerScopes").GetChildren().Select(
                ss => ss.Key).ToList();

            if (scopes.Any())
            {
                if (operation.Security == null)
                    operation.Security = new List<IDictionary<string, IEnumerable<string>>>();

                var oAuthRequirements = new Dictionary<string, IEnumerable<string>>
                {
                    {
                        "oauth2",
                        scopes
                    }
                };

                operation.Security.Add(oAuthRequirements);
            }
        }
    }
}
