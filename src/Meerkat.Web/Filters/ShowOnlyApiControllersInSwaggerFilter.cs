using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meerkat.Web.Filters
{
    public class ShowOnlyApiControllersInSwaggerFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = new Dictionary<string, PathItem>(swaggerDoc.Paths);
            swaggerDoc.Paths.Clear();
            foreach (var path in paths)
            {
                if (path.Key.Contains("Api/", StringComparison.OrdinalIgnoreCase))
                    swaggerDoc.Paths.Add(path);
            }
        }
    }
}
