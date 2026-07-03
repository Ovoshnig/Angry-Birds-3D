using UnityEngine;

public class BirdPowerView : MonoBehaviour
{
    [field: SerializeField] public BirdPowerType PowerType { get; private set; }
    [field: SerializeField] public bool HasPowerParticle { get; private set; } = true;

    public bool WasActivated { get; private set; } = false;

    public void SetWasActivated() => WasActivated = true;
}
