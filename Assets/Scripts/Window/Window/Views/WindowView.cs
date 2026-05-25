using R3;
using UnityEngine;

public class WindowView : MonoBehaviour
{
    public ReadOnlyReactiveProperty<bool> IsActive { get; private set; }

    private void Awake()
    {
        IsActive = Observable
            .EveryValueChanged(gameObject, g => g.activeSelf)
            .ToReadOnlyReactiveProperty(gameObject.activeSelf)
            .AddTo(gameObject);
    }

    public void SetActive(bool isActive) => gameObject.SetActive(isActive);
}
