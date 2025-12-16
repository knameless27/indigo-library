using Microsoft.Extensions.DependencyInjection;
using indigoLibrary.Application.Interfaces;
using indigoLibrary.Application.Services;
using indigoLibrary.Infrastructure.Repositories;
using indigoLibrary.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace indigoLibrary.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<IndigoLibraryDbContext>(options =>
            options.UseInMemoryDatabase("indigoLibraryDb"));


            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILoanRepository, LoanRepository>();

            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ILoanService, LoanService>();



            return services;
        }
    }
}