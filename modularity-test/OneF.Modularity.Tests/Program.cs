// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Microsoft.Extensions.DependencyInjection;

namespace OneF.Modularity.Tests
{
    internal class Program
    {
        private static void Main()
        {
            var application = ModuleFactory.Create<TestModule3>();

            application.ServiceProvider
                .GetService<IService1>()
                ?.Print();

            Console.ReadLine();
        }
    }
}
