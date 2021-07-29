// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace OneF.Modularity
{
    public class ModuleContext : IModuleContext
    {
        public ModuleContext(Type startupModuleType, Action<IConfigurationBuilder>? builder = null)
        {
            StartupModuleType = startupModuleType;
            Services = new ServiceCollection();

            if (builder != null)
            {
                var options = new ConfigurationBuilder();
                builder?.Invoke(options);

                Services.Replace(ServiceDescriptor.Singleton<IConfiguration>(options.Build()));
            }

            Modules = ModuleHelper.LoadModules(Services, StartupModuleType);

            ConfigureServices();

            ServiceProvider = Services.BuildServiceProvider();
        }

        public Type StartupModuleType { get; }

        public IServiceCollection Services { get; private set; }

        public IServiceProvider ServiceProvider { get; private set; }

        public IReadOnlyList<IModuleDescriptor> Modules { get; }

        private void ConfigureServices()
        {
            var context = new ConfigureServiceContext(Services);

            foreach (var moduleDescriptor in Modules)
            {
                if (moduleDescriptor.Instance is Module module)
                {
                    module.Initialization(context);
                }

                ModuleHelper.RegisterModuleAssembly(Services, moduleDescriptor.InstanceAssembly);
            }

            foreach (var module in Modules)
            {
                module.Instance.ConfigureServices(context);
            }

            foreach (var module in Modules)
            {
                module.Instance.PostConfigureServices(context);
            }
        }
    }
}
