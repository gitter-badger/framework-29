// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using OneF.Modularity.DependencyInjection;

namespace OneF.Modularity.Tests
{
    [DependsOn(typeof(TestModule1.TestModule1))]
    [DependsOn(typeof(TestModule2.TestModule2))]
    public class TestModule3 : Module
    {

    }
}
