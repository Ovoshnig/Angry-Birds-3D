using UnityEngine;

[RequireComponent(typeof(PigDestroyerView))]
public class PigEntityView : DestructibleEntityView
{
    protected override void Awake()
    {
        base.Awake();
        DestroyerView = GetComponent<PigDestroyerView>();
    }
}
