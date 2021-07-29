// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using OneF.Modularity.DependencyInjection;

namespace OneF.Modularity.Tests
{
    public interface IService1
    {
        void Print();
    }

    public class Service1 : IService1, ISingletonObject
    {
        public void Print()
        {
            Console.WriteLine("Hello OneF");
        }
    }
}
