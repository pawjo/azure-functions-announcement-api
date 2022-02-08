using AutoMapper;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
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

            builder.Services.AddAutoMapper(cfg =>
                cfg.AddProfile<AnnouncementMappingProfile>());
        }
    }
}
