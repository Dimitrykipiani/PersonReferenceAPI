using Microsoft.Extensions.DependencyInjection;
using TBC.PersonReference.Application.FileService;
using TBC.PersonReference.Application.Mappings;
using TBC.PersonReference.Application.Services.PersonService;
using TBC.PersonReference.Core.Interfaces;
using TBC.PersonReference.Core.UseCases;
using TBC.PersonReference.Infrastructure.Data;
using TBC.PersonReference.Infrastructure.Data.Repositories;

namespace TBC.PersonReference.Application.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IPersonRepository, PersonRepository>();

            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Use Cases
            services.AddTransient<AddPersonUseCase>();
            services.AddTransient<GetPersonByIdUseCase>();
            services.AddTransient<UpdatePersonUseCase>();
            services.AddTransient<DeletePersonUseCase>();
            services.AddTransient<SearchPersonUseCase>();
            services.AddTransient<UploadPersonImageUseCase>();
            services.AddTransient<AddRelatedPersonUseCase>();
            services.AddTransient<RemoveRelatedPersonUseCase>();
            services.AddTransient<GetRelatedPersonReportUseCase>();

            // Services
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IFileService, FileService.FileService>();

            services.AddAutoMapper(typeof(PersonMappingProfile).Assembly);

            return services;
        }
    }
}
