// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Microsoft.Extensions.DependencyInjection;

namespace OneF.Modularity.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DependencyAttribute : Attribute, IInjectionObject
    {
        public DependencyAttribute() { }

        public DependencyAttribute(ServiceLifetime lifetime)
        {
            Lifetime = lifetime;
        }

        public ServiceLifetime? Lifetime { get; set; }

        public bool TryRegister { get; set; }

        public bool ReplaceService { get; set; }

        public bool IsDisabled { get; set; }
        public bool ReplaceFirst { get; internal set; }
    }
}
