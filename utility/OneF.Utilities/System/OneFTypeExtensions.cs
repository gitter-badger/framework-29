// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Diagnostics;

namespace System
{
    [DebuggerStepThrough]
    public static class OneFTypeExtensions
    {
        public static bool IsAssignableTo<T>(this Type type)
        {
            return type.IsAssignableTo(typeof(T));
        }

        public static bool IsAssignableFrom<T>(this Type type)
        {
            return type.IsAssignableFrom(typeof(T));
        }
    }
}
