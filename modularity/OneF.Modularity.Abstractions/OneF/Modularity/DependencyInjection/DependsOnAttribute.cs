// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;

namespace OneF.Modularity.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute : Attribute
    {
        public DependsOnAttribute(params Type[] denpendTypes)
        {
            DenpendTypes = denpendTypes;
        }

        public Type[] DenpendTypes { get; }
    }
}
