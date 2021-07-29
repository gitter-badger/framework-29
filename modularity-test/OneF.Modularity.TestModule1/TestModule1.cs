// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;

namespace OneF.Modularity.TestModule1
{
    public class TestModule1 : Module
    {
        public override void ConfigureServices(ConfigureServiceContext context)
        {
            Console.WriteLine(nameof(TestModule1));
        }
    }
}
