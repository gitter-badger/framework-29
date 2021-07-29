// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Diagnostics;
using System.Linq;

namespace System
{
    [DebuggerStepThrough]
    public static class OneFObjectExtensions
    {
        public static bool IsIn<T>(this T item, params T[] source)
        {
            return source.Contains(item);
        }
    }
}
