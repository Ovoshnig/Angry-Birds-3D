using R3;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public abstract class CompletionPanelView : MonoBehaviour
{
    private readonly Subject<Unit> _shown = new();

    public Observable<Unit> Shown => _shown;

    private void OnDestroy() => _shown.Dispose();

    public void Show()
    {
        gameObject.SetActive(true);
        _shown.OnNext(Unit.Default);
    }

    public void Hide() => gameObject.SetActive(false);
}
