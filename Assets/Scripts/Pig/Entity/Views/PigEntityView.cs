using UnityEngine;

[RequireComponent(typeof(CollisionView))]
[RequireComponent(typeof(PigDestroyerView))]
public class PigEntityView : MonoBehaviour
{
    public CollisionView CollisionView { get; private set; }
    public PigDestroyerView DestroyerView { get; private set; }

    private void Awake()
    {
        CollisionView = GetComponent<CollisionView>();
        DestroyerView = GetComponent<PigDestroyerView>();
    }
}
