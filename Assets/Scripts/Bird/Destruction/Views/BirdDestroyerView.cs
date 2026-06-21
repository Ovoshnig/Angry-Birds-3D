using System.Collections.Generic;
using UnityEngine;

public class BirdDestroyerView : MonoBehaviour
{
    private readonly List<BirdDestroyerView> _cloneDestroyerViews = new();

    public bool IsDestroying { get; private set; } = false;

    public void StartDestroying() => IsDestroying = true;

    public void Destroy()
    {
        Destroy(gameObject);

        foreach (var cloneDestroyerView in _cloneDestroyerViews)
            cloneDestroyerView.Destroy();

        _cloneDestroyerViews.Clear();
    }

    public void AddClone(BirdDestroyerView clone)
    {
        if (clone != null)
            _cloneDestroyerViews.Add(clone);
    }
}
