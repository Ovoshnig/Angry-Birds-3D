using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace VContainer.Extensions
{
    public static class VContainerInstanceInHierarchyExtensions
    {
        public static InstanceInHierarchyRegistrationBuilder<T> RegisterInstanceInHierarchy<T>(this IContainerBuilder builder) where T : Component
        {
            LifetimeScope lifetimeScope = (LifetimeScope)builder.ApplicationOrigin;
            InstanceInHierarchyRegistrationBuilder<T> registrationBuilder = new(lifetimeScope.gameObject.scene);

            builder.RegisterBuildCallback(container =>
            {
                container.Resolve<T>();
            });

            return builder.Register(registrationBuilder);
        }

        public static InstancesInHierarchyRegistrationBuilder<T> RegisterInstancesInHierarchy<T>(this IContainerBuilder builder) where T : Component
        {
            LifetimeScope lifetimeScope = (LifetimeScope)builder.ApplicationOrigin;
            InstancesInHierarchyRegistrationBuilder<T> registrationBuilder = new(lifetimeScope.gameObject.scene);

            builder.RegisterBuildCallback(container =>
            {
                container.Resolve<IReadOnlyList<T>>();
            });

            return builder.Register(registrationBuilder);
        }
    }
}
