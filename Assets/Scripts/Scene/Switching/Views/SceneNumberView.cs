using R3;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public sealed class SceneNumberView : UIView
{
    private TMP_Text _text;
    private SceneButtonView _buttonView;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _buttonView = GetComponentInParent<SceneButtonView>();
    }

    private void Start()
    {
        _buttonView.IsInteractable
            .Subscribe(isInteractable => _text.enabled = isInteractable)
            .AddTo(this);
    }
}
