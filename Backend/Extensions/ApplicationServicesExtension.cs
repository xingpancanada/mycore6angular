using System.Globalization;
using Backend.Errors;
using Backend.Interfaces;
using Backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServicesFromExtension(this IServiceCollection services)
        {
            ////23.adding a repository and interface
            services.AddScoped<IProductRepository, ProductRepository>();

            /////54. Improving the validation error responses: errors array
            services.Configure<ApiBehaviorOptions>(options => {
                options.InvalidModelStateResponseFactory = actionContext => {
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse{
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });


            return services;
        }
    }
}