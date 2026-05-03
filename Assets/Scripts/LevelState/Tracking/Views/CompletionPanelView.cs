using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CompletionPanelView : MonoBehaviour
{
    private void Start() => SetActive(false);

    public void SetActive(bool value) => gameObject.SetActive(value);
}
