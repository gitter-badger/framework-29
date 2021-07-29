// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace OneF
{
    [DebuggerStepThrough]
    public static class RandomHelper
    {
        private static readonly Random Rnd = new Random();

        public static int GetRandom(int minValue, int maxValue)
        {
            lock (Rnd)
            {
                return Rnd.Next(minValue, maxValue);
            }
        }

        public static int GetRandom(int maxValue)
        {
            lock (Rnd)
            {
                return Rnd.Next(maxValue);
            }
        }

        public static int GetRandom()
        {
            lock (Rnd)
            {
                return Rnd.Next();
            }
        }

        public static T GetRandomOf<T>([NotNull] params T[] objs)
        {
            Check.NotNullOrEmpty(objs, nameof(objs));

            return objs[GetRandom(0, objs.Length)];
        }

        public static T GetRandomOfList<T>([NotNull] IList<T> list)
        {
            Check.NotNullOrEmpty(list, nameof(list));

            return list[GetRandom(0, list.Count)];
        }

        public static List<T> GenerateRandomizedList<T>([NotNull] IEnumerable<T> items)
        {
            Check.NotNull(items, nameof(items));

            var currentList = new List<T>(items);
            var randomList = new List<T>();

            while (currentList.Any())
            {
                var randomIndex = RandomHelper.GetRandom(0, currentList.Count);
                randomList.Add(currentList[randomIndex]);
                currentList.RemoveAt(randomIndex);
            }

            return randomList;
        }
    }
}
