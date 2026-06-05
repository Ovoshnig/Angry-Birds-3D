using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VContainer.Extensions
{
    public sealed class FindInstanceProvider<T> : IInstanceProvider where T : Component
    {
        private readonly Scene _scene;

        public FindInstanceProvider(Scene scene) => _scene = scene;

        public object SpawnInstance(IObjectResolver resolver)
        {
            if (!_scene.IsValid())
                throw new VContainerException(typeof(T), $"Invalid find target scene for instance of type {typeof(T).Name}");

            List<GameObject> rootGameObjects = new();
            _scene.GetRootGameObjects(rootGameObjects);

            foreach (GameObject root in rootGameObjects)
            {
                T component = root.GetComponentInChildren<T>(true);
                if (component != null)
                    return component;
            }

            throw new VContainerException(typeof(T), $"Component of type {typeof(T).Name} was not found in scene {_scene.name}");
        }
    }
}
