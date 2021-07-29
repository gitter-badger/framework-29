// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static bool IsAdded<T>(this IServiceCollection services)
        {
            return services.Any(d => d.ServiceType == typeof(T));
        }

        public static T? GetInstanceOrNull<T>(this IServiceCollection services)
        {
            return (T?)services
                .FirstOrDefault(d => d.ServiceType == typeof(T))
                ?.ImplementationInstance;
        }

        public static T GetRequiredInstance<T>(this IServiceCollection services)
        {
            var service = services.GetInstanceOrNull<T>();

            if (service == null)
            {
                throw new InvalidOperationException("Could not find singleton service: " + typeof(T).AssemblyQualifiedName);
            }

            return service;
        }
    }
}
