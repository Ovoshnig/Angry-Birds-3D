using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenView : MonoBehaviour
{
    private Scrollbar _scrollbar;

    private void Awake() => _scrollbar = GetComponentInChildren<Scrollbar>();

    public void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);

    public void SetProgress(float progress) => _scrollbar.size = progress;
}
