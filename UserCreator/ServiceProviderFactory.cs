using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using UserCreator.Model;

namespace UserCreator
{
    public class ServiceProviderFactory
    {
        public static ServiceProvider BuildServiceProvider(string connectionString)
        {
            var services = new ServiceCollection();

            services.AddDbContext<AppDbContext>(options => 
            {
                options.UseSqlServer(connectionString);
            });

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options => 
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
            });

            services.AddLogging();

            services.AddScoped<UserManager<User>>();
            services.AddScoped<UserService>();

            var provider = services.BuildServiceProvider();
            return provider;
        }
    }
}
