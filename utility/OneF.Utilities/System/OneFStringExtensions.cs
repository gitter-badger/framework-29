// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Diagnostics;

namespace System
{
    [DebuggerStepThrough]
    public static class OneFStringExtensions
    {
        public static bool IsNullOrEmpty(this string? str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string? str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static void CheckLength(this string? src, string parameterName, uint maxLength, uint minLength = uint.MinValue)
        {
            if (src.IsNullOrEmpty())
            {
                throw new ArgumentNullException(parameterName);
            }

            if (minLength > 0 && src!.Length < minLength)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }

            if (src!.Length > maxLength)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }
    }
}
