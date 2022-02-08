using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(AnnouncementsApiFunctionsApp.Startup))]

namespace AnnouncementsApiFunctionsApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

            builder.Services.AddDbContext<AnnouncementFAContext>(options =>
                SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));
        }
    }
}
