using DataAccess.IRepository;
using DataAccess.Repository;
using DataAccess.Service;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Core
{
    public static class DependencyInjection
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            // Register the repository and the service
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<AccountService>();

            // Other service registrations
        }
    }
}
