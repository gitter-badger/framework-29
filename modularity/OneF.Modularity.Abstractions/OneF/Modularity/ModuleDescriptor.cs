// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace OneF.Modularity
{
    public class ModuleDescriptor : IModuleDescriptor
    {
        private readonly List<IModuleDescriptor> _dependencies = new();

        public ModuleDescriptor([NotNull] IModule instance)
        {
            Instance = Check.NotNull(instance, nameof(instance));
            InstanceType = instance.GetType();
            InstanceAssembly = InstanceType.Assembly;
        }

        public Type InstanceType { get; set; }

        public Assembly InstanceAssembly { get; set; }

        public IModule Instance { get; set; }

        public IReadOnlyList<IModuleDescriptor> Dependencies => _dependencies;

        public void AddDependency(IModuleDescriptor descriptor) => _dependencies.AddIfNotContains(descriptor);

        public override string ToString()
        {
            return $"Module descriptor: {InstanceType.FullName}";
        }
    }
}
