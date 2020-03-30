using System.Web.Http;
using WebActivatorEx;
using CentralAtivos.API;
using Swashbuckle.Application;
using System;
using System.Linq;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace CentralAtivos.API
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "Saraf - Central de Ativos API");

                        c.IgnoreObsoleteActions();
                        c.IncludeXmlComments($@"{AppDomain.CurrentDomain.BaseDirectory}\bin\Swagger.xml");
                        c.IgnoreObsoleteProperties();

                        c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                    })
                .EnableSwaggerUi(c =>
                    {

                        c.DocumentTitle("Exemplo de utilização da API da Central de Ativos");
                        c.DocExpansion(DocExpansion.List);

                    });
        }
    }
}
