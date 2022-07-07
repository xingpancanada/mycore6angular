using System.Globalization;
using Backend.Errors;
using Backend.Interfaces;
using Backend.Repositories;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServicesFromExtension(this IServiceCollection services)
        {
            ////174.testing the token
            services.AddScoped<ITokenService, TokenService>();

            ////23.adding a repository and interface
            services.AddScoped<IProductRepository, ProductRepository>();

            /////138. Creating a basket repository interface
            services.AddScoped<IBasketRepository, BasketRepository>();

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