using UnityEngine;

[RequireComponent(typeof(CollisionView))]
[RequireComponent(typeof(PigDestroyerView))]
[RequireComponent(typeof(PigSFXPlayerView))]
public class PigEntityView : MonoBehaviour
{
    public CollisionView CollisionView { get; private set; }
    public PigDestroyerView DestroyerView { get; private set; }
    public PigSFXPlayerView SFXPlayerView { get; private set; }

    private void Awake()
    {
        CollisionView = GetComponent<CollisionView>();
        DestroyerView = GetComponent<PigDestroyerView>();
        SFXPlayerView = GetComponent<PigSFXPlayerView>();
    }
}
