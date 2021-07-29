// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Reflection;
using OneF.Modularity;
using OneF.Modularity.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void ApplyModule<TModule>(this IServiceCollection services)
        {
            var context = new ConfigureServiceContext(services);
            // TODO: here
            // 1. 反射拿到TModule的DependsOn
            var dependsOns = typeof(TModule).GetCustomAttribute<DependsOnAttribute>()?.DenpendTypes;
            // 2. 通过拓扑排序，将所有依赖加入集合
            // 3. 并顺序执行对应的ConfigureServices

        }
    }
}
