using Api.Exceptions.Handlers;
using Application.Behaviors;
using Application.Handlers.Trucks;
using Application.Requests.Trucks;
using Application.Validators.Trucks;
using FluentValidation;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using System.Reflection;

namespace Api
{
    public class IoC
    {
        public static void RegisteredServices(IServiceCollection services, ConfigurationManager configurationManager)
        {
            RegisterDbContext(services, configurationManager);

            RegisterSwagger(services);

            RegisterValidators(services);

            RegisterMediatR(services);

            RegisterExceptionHandlers(services);

            services.AddControllers();
        }

        private static void RegisterExceptionHandlers(IServiceCollection services)
        {
            services.AddExceptionHandler<ValidationExceptionHandler>();
            services.AddProblemDetails();
        }

        private static void RegisterMediatR(IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<CreateTruckHandler>();
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
        }

        private static void RegisterValidators(IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateTruckRequest>, CreateTruckValidator>();
            services.AddScoped<IValidator<UpdateTruckRequest>, UpdateTruckValidator>();
            services.AddScoped<IValidator<RemoveTruckRequest>, RemoveTruckValidator>();
            services.AddScoped<IValidator<GetTruckByIdRequest>, GetTruckByIdValidator>();
            services.AddScoped<IValidator<GetTruckListRequest>, GetTruckListValidator>();
        }

        private static void RegisterSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Coldrun task",
                    Description = "Create the first REST API module of an ERP application responsible for managing Truck data. " +
                    "This API will serve as a foundation for future modules that will manage other resources such as employees, factories, and customers.",
                    Contact = new OpenApiContact
                    {
                        Name = "Maciej Hałas",
                        Email = "maciej.halas@outlook.com"
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        private static void RegisterDbContext(IServiceCollection services, ConfigurationManager configurationManager)
        {
            var connectionString = configurationManager.GetConnectionString("ColdrunConnection");
            services.AddDbContext<ColdrunContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}
