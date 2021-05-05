using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Behaviours;
using System.Reflection;

namespace Ordering.Application
{
    //This class will provide all the dependecy injections for the Ordering.Application in the Ordering.API Startup class.
    //This way we follow the Clean Architecture implementation and group all the dependecies for each layer.
    public static class ApplicationServiceRegistration
    {
        //This method will include all the required services/packages and will be injected in the Ordering.API Startup class.
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) 
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }

    }
}
