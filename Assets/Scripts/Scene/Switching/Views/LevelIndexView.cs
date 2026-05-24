using R3;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public sealed class LevelIndexView : UIView
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

#if UNITY_EDITOR
    public void SetIndex(int index)
    {
        if (UnityEditor.EditorApplication.isPlaying)
            return;

        GetComponent<TMP_Text>().SetText("{0}", index);
    }
#endif
}
