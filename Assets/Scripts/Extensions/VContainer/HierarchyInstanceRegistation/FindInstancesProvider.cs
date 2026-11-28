using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Ovoshnig.Extensions.VContainer;

public sealed class FindInstancesProvider<T>(Scene scene) : IInstanceProvider where T : Component
{
    private readonly Scene _scene = scene;

    public object SpawnInstance(IObjectResolver resolver)
    {
        if (!_scene.IsValid())
        {
            throw new VContainerException(typeof(T),
                $"Invalid find target scene for instances of type {typeof(T).Name}");
        }

        List<T> components = [];
        List<T> buffer = [];
        List<GameObject> rootGameObjects = [];
        _scene.GetRootGameObjects(rootGameObjects);

        foreach (GameObject root in rootGameObjects)
        {
            root.GetComponentsInChildren(true, buffer);
            components.AddRange(buffer);
        }

        return components;
    }
}
