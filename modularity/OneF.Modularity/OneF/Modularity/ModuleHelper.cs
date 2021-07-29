// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OneF.Modularity.DependencyInjection;

namespace OneF.Modularity
{
    // TODO: 待优化
    internal static class ModuleHelper
    {
        public static IReadOnlyList<IModuleDescriptor> LoadModules(
            [NotNull] IServiceCollection services,
            [NotNull] Type startupModuleType)
        {
            Check.NotNull(services, nameof(services));
            Check.NotNull(startupModuleType, nameof(startupModuleType));

            var modules = FillModules(startupModuleType);

            foreach (var module in modules)
            {
                FillDependencies(modules, module);
            }

            var descriptors = modules.Cast<IModuleDescriptor>().ToList();

            SortByDependency(descriptors, startupModuleType);

            return descriptors;
        }

        private static List<IModuleDescriptor> SortByDependency(List<IModuleDescriptor> modules, Type startupModuleType)
        {
            var sortedModules = modules.SortByDependencies(m => m.Dependencies);

            sortedModules.MoveItem(m => m.InstanceType == startupModuleType, modules.Count - 1);

            return sortedModules;
        }

        private static IEnumerable<ModuleDescriptor> FillModules(Type moduleType)
        {
            foreach (var item in FindAllModuleTypes(moduleType))
            {
                var newModule = (IModule?)Activator.CreateInstance(item);
                if (newModule != null)
                {
                    yield return new ModuleDescriptor(newModule);
                }
            }
        }

        private static void FillDependencies(IEnumerable<ModuleDescriptor> modules, ModuleDescriptor module)
        {
            var moduleTypes = module.InstanceType.GetCustomAttributes<DependsOnAttribute>()
                .SelectMany(x => x.DenpendTypes)
                .Distinct();

            foreach (var moduleType in moduleTypes)
            {
                var depended = modules.FirstOrDefault(x => x.InstanceType == moduleType);
                if (depended == null)
                {
                    throw new ArgumentNullException(nameof(depended), $"Can not find a depended module {moduleType.AssemblyQualifiedName} for {module.InstanceType.AssemblyQualifiedName}");
                }

                module.AddDependency(depended);
            }
        }

        public static IEnumerable<Type> FindAllModuleTypes(Type moduleType)
        {
            CheckModule(moduleType);

            var moduleTypes = new List<Type>();

            AddModuleAndDependenciesRecursively(moduleTypes, moduleType);

            return moduleTypes;
        }

        public static bool IsModule(Type moduleType)
        {
            return moduleType.IsClass
                 && !moduleType.IsAbstract
                 && !moduleType.IsGenericType
                 && moduleType.IsAssignableTo<IModule>();
        }

        public static void CheckModule(Type moduleType)
        {
            if (!IsModule(moduleType))
            {
                throw new ArgumentException($"Given type is not an {typeof(IModule).AssemblyQualifiedName}", nameof(moduleType));
            }
        }

        private static void AddModuleAndDependenciesRecursively(
            ICollection<Type> moduleTypes,
            Type moduleType)
        {
            CheckModule(moduleType);

            if (moduleTypes.Contains(moduleType))
            {
                return;
            }

            moduleTypes.Add(moduleType);

            var dependTypes = moduleType.GetCustomAttributes<DependsOnAttribute>()
                .SelectMany(x => x.DenpendTypes);

            foreach (var item in dependTypes)
            {
                AddModuleAndDependenciesRecursively(moduleTypes, item);
            }
        }

        public static void RegisterModuleAssembly(IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypes()
                    .Where(x => x.IsClass
                    && !x.IsAbstract
                    && !x.IsGenericType
                    && x.IsAssignableTo<IInjectionObject>());

            foreach (var item in types)
            {
                AddType(services, item);
            }
        }

        private static void AddType(IServiceCollection services, Type type)
        {
            var dependencyAttribute = type.GetCustomAttribute<DependencyAttribute>(true);
            if (dependencyAttribute?.IsDisabled == true)
            {
                return;
            }

            var lifetime = GetServiceLifetiem(type, dependencyAttribute);
            if (lifetime == null)
            {
                return;
            }

            var exposedTyeps = GetExposedServices(type);

            foreach (var exposedType in exposedTyeps)
            {
                var serviceDescriptor = CreateServiceDescriptor(
                    exposedType,
                    type,
                    exposedTyeps,
                    lifetime.Value);

                if (dependencyAttribute?.ReplaceFirst == true)
                {
                    services.Replace(serviceDescriptor);
                }
                else if (dependencyAttribute?.TryRegister == true)
                {
                    services.TryAdd(serviceDescriptor);
                }
                else
                {
                    services.Add(serviceDescriptor);
                }
            }
        }

        private static ServiceLifetime? GetServiceLifetiem(Type type, DependencyAttribute? dependencyAttribute)
        {
            return dependencyAttribute?.Lifetime ?? GetServiceLifetimeFromInterface(type);
        }

        private static ServiceLifetime? GetServiceLifetimeFromInterface(Type type)
        {
            if (typeof(ITransientObject).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Transient;
            }

            if (typeof(ISingletonObject).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Singleton;
            }

            if (typeof(IScopedObject).GetTypeInfo().IsAssignableFrom(type))
            {
                return ServiceLifetime.Scoped;
            }

            return null;
        }

        private static IEnumerable<Type> GetExposedServices(Type type)
        {
            var interfaces = type.GetTypeInfo().GetInterfaces().Distinct();
            var self = Enumerable.Empty<Type>();

            if (!interfaces.Any())
            {
                self = new List<Type>(1) { type };
            }

            return interfaces.Concat(self);
        }

        private static ServiceDescriptor CreateServiceDescriptor(
            Type exposedType,
            Type implementationType,
            IEnumerable<Type> allExposedTypes,
            ServiceLifetime lifetime)
        {
            if (lifetime.IsIn(ServiceLifetime.Singleton, ServiceLifetime.Scoped))
            {
                var redirectedType = GetRedirectedTypeOrNull(
                    exposedType,
                    implementationType,
                    allExposedTypes
                );

                if (redirectedType != null)
                {
                    return ServiceDescriptor.Describe(
                        exposedType,
                        provider => provider.GetService(redirectedType)!,
                        lifetime
                    );
                }
            }

            return ServiceDescriptor.Describe(
                exposedType,
                implementationType,
                lifetime
            );
        }

        private static Type? GetRedirectedTypeOrNull(
            Type exposedType,
            Type implementationType,
            IEnumerable<Type> allExposedTypes)
        {
            if (allExposedTypes.Count() < 2)
            {
                return null;
            }

            if (exposedType == implementationType)
            {
                return null;
            }

            if (allExposedTypes.Contains(implementationType))
            {
                return implementationType;
            }

            return allExposedTypes.FirstOrDefault(x => x != exposedType && exposedType.IsAssignableFrom(x));
        }
    }
}
