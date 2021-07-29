// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Diagnostics;
using OneF;

namespace System.Collections.Generic
{
    [DebuggerStepThrough]
    public static class OneFDictionaryExtensions
    {
        public static TValue GetOrAdd<TKey, TValue>(
            this IDictionary<TKey, TValue> directory,
             TKey key,
            TValue value)
        {
            return GetOrAdd(directory, key, () => value);
        }

        public static TValue GetOrAdd<TKey, TValue>(
            this IDictionary<TKey, TValue> directory,
           TKey key,
            Func<TValue> factory)
        {
            return GetOrAdd(directory, key, k => factory());
        }

        public static TValue GetOrAdd<TKey, TValue>(
            this IDictionary<TKey, TValue> directory,
            TKey key,
            Func<TKey, TValue> factory)
        {
            Check.NotNull(key, nameof(key));

            if (directory.TryGetValue(key, out var result))
            {
                return result;
            }

            return directory[key] = factory(key);
        }
    }
}
