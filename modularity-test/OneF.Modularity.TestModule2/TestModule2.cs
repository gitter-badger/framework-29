// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;

namespace OneF.Modularity.TestModule2
{
    public class TestModule2 : Module
    {
        public override void ConfigureServices(ConfigureServiceContext context)
        {
            Console.WriteLine(nameof(TestModule2));

            Configure<TestOptions>(options =>
            {
                options.Name = nameof(TestModule2);
            });
        }
    }

    public class TestOptions
    {
        public string Name { get; set; }
    }
}
