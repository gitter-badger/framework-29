// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OneF.Modularity
{
    public abstract class Module : IModule
    {
        protected ConfigureServiceContext Context { get; private set; }

        public void Initialization(ConfigureServiceContext context)
        {
            Context = context;
        }

        public virtual void ConfigureServices(ConfigureServiceContext context)
        {
        }

        public virtual void PostConfigureServices(ConfigureServiceContext context)
        {
        }

        /// <inheritdoc cref="OptionsServiceCollectionExtensions.Configure{TOptions}(IServiceCollection, Action{TOptions})"/>
        protected void Configure<TOptions>(Action<TOptions> configureOptions)
            where TOptions : class
        {
            Context.Services.Configure(configureOptions);
        }

        /// <inheritdoc cref="OptionsServiceCollectionExtensions.Configure{TOptions}(IServiceCollection, string, Action{TOptions})"/>
        protected void Configure<TOptions>(string name, Action<TOptions> configureOptions)
            where TOptions : class
        {
            Context.Services.Configure(name, configureOptions);
        }

        /// <inheritdoc cref="OptionsServiceCollectionExtensions.ConfigureAll{TOptions}(IServiceCollection, Action{TOptions})"/>
        protected void ConfigureAll<TOptions>(Action<TOptions> configureOptions)
            where TOptions : class
        {
            Context.Services.ConfigureAll(configureOptions);
        }

        /// <inheritdoc cref="OptionsConfigurationServiceCollectionExtensions.Configure{TOptions}(IServiceCollection, IConfiguration)"/>
        protected void Configure<TOptions>(IConfiguration configuration)
            where TOptions : class
        {
            Context.Services.Configure<TOptions>(configuration);
        }

        /// <inheritdoc cref="OptionsConfigurationServiceCollectionExtensions.Configure{TOptions}(IServiceCollection, IConfiguration, Action{BinderOptions})"/>
        protected void Configure<TOptions>(IConfiguration configuration, Action<BinderOptions> configureBinder)
            where TOptions : class
        {
            Context.Services.Configure<TOptions>(configuration, configureBinder);
        }

        /// <inheritdoc cref="OptionsConfigurationServiceCollectionExtensions.Configure{TOptions}(IServiceCollection, string, IConfiguration)"/>
        protected void Configure<TOptions>(string name, IConfiguration configuration)
            where TOptions : class
        {
            Context.Services.Configure<TOptions>(name, configuration);
        }

        /// <inheritdoc cref="OptionsConfigurationServiceCollectionExtensions.Configure{TOptions}(IServiceCollection, string, IConfiguration, Action{BinderOptions})"/>
        protected void Configure<TOptions>(string name, IConfiguration configuration, Action<BinderOptions> configureBinder)
            where TOptions : class
        {
            Context.Services.Configure<TOptions>(name, configuration, configureBinder);
        }

        /// <inheritdoc cref="OptionsServiceCollectionExtensions.PostConfigure{TOptions}(IServiceCollection, Action{TOptions})"/>
        protected void PostConfigure<TOptions>(Action<TOptions> configureOptions)
            where TOptions : class
        {
            Context.Services.PostConfigure(configureOptions);
        }

        /// <inheritdoc cref="OptionsServiceCollectionExtensions.PostConfigure{TOptions}(IServiceCollection, string, Action{TOptions})"/>
        protected void PostConfigure<TOptions>(string name, Action<TOptions> configureOptions)
            where TOptions : class
        {
            Context.Services.PostConfigure(name, configureOptions);
        }

        /// <inheritdoc cref="OptionsServiceCollectionExtensions.PostConfigureAll{TOptions}(IServiceCollection, Action{TOptions})"/>
        protected void PostConfigureAll<TOptions>(Action<TOptions> configureOptions)
            where TOptions : class
        {
            Context.Services.PostConfigureAll(configureOptions);
        }
    }
}
