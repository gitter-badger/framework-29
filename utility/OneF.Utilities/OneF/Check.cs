// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace OneF
{
    [DebuggerStepThrough]
    public static class Check
    {
        public static T NotNull<T>(T? value, string parameterName, string? error = null)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName, error);
            }

            return value;
        }

        public static IEnumerable<T>? NotNullOrEmpty<T>(IEnumerable<T>? value, [NotNull] string parameterName)
        {
            if (value.IsNullOrEmpty())
            {
                throw new ArgumentException(parameterName + " can not be null or empty!", parameterName);
            }

            return value;
        }
    }
}
