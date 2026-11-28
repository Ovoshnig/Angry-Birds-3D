using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.Extensions.VContainer;

public static class VContainerInstanceInHierarchyExtensions
{
    extension(IContainerBuilder builder)
    {
        public InstanceInHierarchyRegistrationBuilder<T> RegisterInstanceInHierarchy<T>() where T : Component
        {
            LifetimeScope lifetimeScope = (LifetimeScope)builder.ApplicationOrigin;
            InstanceInHierarchyRegistrationBuilder<T> registrationBuilder = new(
                lifetimeScope.gameObject.scene);

            builder.RegisterBuildCallback(container => container.Resolve<T>());
            return builder.Register(registrationBuilder);
        }

        public InstancesInHierarchyRegistrationBuilder<T> RegisterInstancesInHierarchy<T>()
            where T : Component
        {
            LifetimeScope lifetimeScope = (LifetimeScope)builder.ApplicationOrigin;
            InstancesInHierarchyRegistrationBuilder<T> registrationBuilder = new(
                lifetimeScope.gameObject.scene);

            builder.RegisterBuildCallback(container => container.Resolve<IReadOnlyList<T>>());
            return builder.Register(registrationBuilder);
        }
    }
}
