using UnityEngine;

public class BirdDestroyerView : MonoBehaviour
{
    public bool IsDestroying { get; private set; } = false;

    public void StartDestroying() => IsDestroying = true;

    public void Destroy() => Destroy(gameObject);
}
