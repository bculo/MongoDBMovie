﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TBP.Interfaces;

namespace TBP.Configurations
{
    public static class ServiceCollectionExtensions
    {
        public static void InstallConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var configList = typeof(IInstaller).Assembly.GetExportedTypes()
                .Where(item => typeof(IInstaller).IsAssignableFrom(item) && !item.IsAbstract && !item.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>()
                .ToList();

            configList.ForEach(item => item.Configure(services, configuration));
        }
    }
}
