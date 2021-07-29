// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Reflection;

namespace OneF.Modularity
{
    public interface IModuleDescriptor
    {
        Type InstanceType { get; }

        Assembly InstanceAssembly { get; }

        IModule Instance { get; }

        IReadOnlyList<IModuleDescriptor> Dependencies { get; }
    }
}
