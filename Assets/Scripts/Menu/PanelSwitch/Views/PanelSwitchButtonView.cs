using R3;
using UnityEngine;

public abstract class PanelSwitchButtonView : ButtonView
{
    [SerializeField] private GameObject _currentPanel;
    [SerializeField] private GameObject _newPanel;

    private void OnValidate()
    {
        if (_currentPanel == null)
            _currentPanel = transform.parent.gameObject;
    }

    protected virtual void Awake()
    {
        Clicked
            .Subscribe(_ => Switch())
            .AddTo(this);
    }

    public void Switch()
    {
        _newPanel.SetActive(true);
        _currentPanel.SetActive(false);
    }
}
