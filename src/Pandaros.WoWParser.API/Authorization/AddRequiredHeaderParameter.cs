using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Pandaros.WoWParser.API.Authorization
{
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            if (context.MethodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Length == 0)
            {
                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "panda-user",
                    In = ParameterLocation.Header,
                    Style = ParameterStyle.Simple,
                    Required = true,
                    Description = "Email Address"
                });

                operation.Parameters.Add(new OpenApiParameter()
                {
                    Name = "panda-token",
                    In = ParameterLocation.Header,
                    Style = ParameterStyle.Simple,
                    Required = true,
                    Description = "Auth Token"
                });
            }
        }
    }
}
