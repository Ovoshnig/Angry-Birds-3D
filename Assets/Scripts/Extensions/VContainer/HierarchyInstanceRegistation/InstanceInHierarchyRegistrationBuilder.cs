using UnityEngine;
using UnityEngine.SceneManagement;

namespace VContainer.Extensions
{
    public sealed class InstanceInHierarchyRegistrationBuilder<T> : RegistrationBuilder where T : Component
    {
        private readonly Scene _scene;

        public InstanceInHierarchyRegistrationBuilder(Scene scene)
            : base(typeof(T), Lifetime.Singleton)
        {
            _scene = scene;
            As(typeof(T));
        }

        public override Registration Build() =>
            new(typeof(T), Lifetime.Singleton, InterfaceTypes, new FindInstanceProvider<T>(_scene), Key);
    }
}
