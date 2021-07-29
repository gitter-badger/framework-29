// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using OneF;

namespace System.Collections.Generic
{
    [DebuggerStepThrough]
    public static class OneFEnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source)
        {
            return source?.Any() == false;
        }

        public static string JoinAsString<T>([NotNull] this IEnumerable<T> source, string separator)
        {
            Check.NotNull(source, nameof(source));

            return string.Join(separator, source);
        }

        public static string JoinAsString<T>(this IEnumerable<T> source, char separator)
        {
            Check.NotNull(source, nameof(source));

            return string.Join(separator, source);
        }

        public static IEnumerable<T> WhereIf<T>(
            [NotNull] this IEnumerable<T> source,
            bool condition,
            Func<T, bool> predicate)
        {
            Check.NotNull(source, nameof(source));

            return condition ? source.Where(predicate) : source;
        }

        public static IEnumerable<T> WhereIfElse<T>(
            [NotNull] this IEnumerable<T> source,
            bool condition,
            Func<T, bool> @true,
            Func<T, bool> @false)
        {
            Check.NotNull(source, nameof(source));

            return condition ? source.Where(@true) : source.Where(@false);
        }
    }
}
