using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DIServiceExtensions
    {
        public static void AddSpaDatasource(this IServiceCollection services, SpaDatasource.Helpers.Options options)
        {
            services.AddScoped<SpaDatasource.Interfaces.ISpaDatasource>((s) => 
                {
                    SpaDatasource.Implementations.SpaDatasource dbConntext = new SpaDatasource.Implementations.SpaDatasource(options.ConnectionString);
                    return dbConntext;
                });
        }

        public static void AddUserManager(this IServiceCollection services)
        {
            services.AddScoped<SpaDatasource.Interfaces.IUserManager, SpaDatasource.Implementations.SpaUserManager>();
        }

    }
}
