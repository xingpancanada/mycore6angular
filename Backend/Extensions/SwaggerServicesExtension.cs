using Microsoft.OpenApi.Models;

namespace Backend.Extensions
{
    public static class SwaggerServicesExtension
    {
        public static IServiceCollection AddSwaggerServicesFromExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                ///////57. Cleaning up the Startup class
                c.SwaggerDoc("v1", new OpenApiInfo{ Title = "API", Version = "v1" });


                ////185. Updating swagger config for identity
                var securitySchema = new OpenApiSecurityScheme{
                    Description= "JWT Auth Bearer Scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference{
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);
                var securityRequirement = new OpenApiSecurityRequirement{{securitySchema, new []{"Bearer"}}};
                c.AddSecurityRequirement(securityRequirement);
            });

            return services;
        }


         ///////57. Cleaning up the Startup class
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1")); 

            return app;
        }
    }
}