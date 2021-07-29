// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace OneF.Modularity
{
    public class ConfigureServiceContext
    {
        public ConfigureServiceContext([NotNull] IServiceCollection services)
        {
            Services = Check.NotNull(services, nameof(services));
        }

        public IServiceCollection Services { get; }
    }
}
