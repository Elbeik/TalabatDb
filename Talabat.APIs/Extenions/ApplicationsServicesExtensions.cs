using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Domine.IRepository;
using Talabat.Domine.IServices;
using Talabat.Repository;
using Talabat.Services;

namespace Talabat.APIs.Extentions
{
    public static class ApplicationsServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UniteOfWork>();

            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped(typeof(IBasketRepositry), typeof(BasketRepository));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddAutoMapper(typeof(MappingProfile));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(E => E.Value.Errors.Count() > 0)
                                                  .SelectMany(E => E.Value.Errors)
                                                  .Select(E => E.ErrorMessage)
                                                  .ToArray();

                    var valdidationErrorResponse = new ApiValidationErrorRespone()
                    {
                        ErrorsResponse = errors
                    };

                    return new BadRequestObjectResult(valdidationErrorResponse);
                };

            });

            return services;

        }
    }
}
