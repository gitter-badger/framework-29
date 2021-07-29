// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using OneF;

namespace System.Collections.Generic
{
    [DebuggerStepThrough]
    public static class OneFCollectionExtensions
    {
        public static void AddIfNotContains<T>([NotNull] this ICollection<T> source, T item)
        {
            Check.NotNull(source, nameof(source));

            if (!source.Contains(item))
            {
                source.Add(item);
            }
        }

        public static void AddIfNotContains<T>([NotNull] this ICollection<T> source, IEnumerable<T> items)
        {
            Check.NotNull(source, nameof(source));

            foreach (var item in items)
            {
                if (!source.Contains(item))
                {
                    source.Add(item);
                }
            }
        }
    }
}
