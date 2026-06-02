using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VContainer.Extensions
{
    public sealed class FindInstancesProvider<T> : IInstanceProvider where T : Component
    {
        private readonly Scene _scene;

        public FindInstancesProvider(Scene scene) => _scene = scene;

        public object SpawnInstance(IObjectResolver resolver)
        {
            if (!_scene.IsValid())
                throw new VContainerException(typeof(T), $"Invalid find target scene for instances of type {typeof(T).Name}");

            List<T> components = new();
            List<GameObject> rootGameObjects = new();
            _scene.GetRootGameObjects(rootGameObjects);

            foreach (GameObject root in rootGameObjects)
            {
                T[] found = root.GetComponentsInChildren<T>(true);
                if (found != null && found.Length > 0)
                    components.AddRange(found);
            }

            return components;
        }
    }
}
