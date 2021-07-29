// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace OneF.Modularity
{
    public interface IModuleContainer
    {
        IReadOnlyList<IModuleDescriptor> Modules { get; }
    }
}
