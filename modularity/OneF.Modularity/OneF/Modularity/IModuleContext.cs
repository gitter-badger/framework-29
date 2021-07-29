// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Microsoft.Extensions.DependencyInjection;

namespace OneF.Modularity
{
    public interface IModuleContext : IModuleContainer
    {
        Type StartupModuleType { get; }

        IServiceCollection Services { get; }

        IServiceProvider ServiceProvider { get; }
    }
}
