// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Microsoft.Extensions.Configuration;

namespace OneF.Modularity
{
    public static class ModuleFactory
    {
        public static IModuleContext Create<TStartupModule>(Action<IConfigurationBuilder>? buider = null)
        {
            return new ModuleContext(typeof(TStartupModule), buider);
        }
    }
}
