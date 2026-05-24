using R3;
using UnityEngine;

public abstract class PanelSwitchButtonView : ButtonView
{
    [SerializeField] private GameObject _currentPanel;
    [SerializeField] private GameObject _newPanel;

    protected virtual void Reset()
    {
        if (transform.parent != null)
            _currentPanel = transform.parent.gameObject;
    }

    protected virtual void Start()
    {
        Clicked
            .Subscribe(_ => Switch())
            .RegisterTo(destroyCancellationToken);
    }

    public void Switch()
    {
        _newPanel.SetActive(true);
        _currentPanel.SetActive(false);
    }
}
