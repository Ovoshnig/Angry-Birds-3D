using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VContainer.Extensions
{
    public sealed class InstancesInHierarchyRegistrationBuilder<T> : RegistrationBuilder where T : Component
    {
        private readonly Scene _scene;

        public InstancesInHierarchyRegistrationBuilder(Scene scene)
            : base(typeof(IReadOnlyList<T>), Lifetime.Singleton)
        {
            _scene = scene;
            As(typeof(IReadOnlyList<T>));
            As(typeof(IEnumerable<T>));
        }

        public override Registration Build() =>
            new(typeof(IReadOnlyList<T>), Lifetime.Singleton, InterfaceTypes, new FindInstancesProvider<T>(_scene), Key);
    }
}
