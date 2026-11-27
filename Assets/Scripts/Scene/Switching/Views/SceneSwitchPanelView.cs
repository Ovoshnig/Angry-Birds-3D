using Ovoshnig.Extensions.UIToolkit;
using Ovoshnig.UI.Basic;
using R3;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ovoshnig.Scene.Switching;

public class SceneSwitchPanelView : UIView
{
    [SerializeField] private VisualElementReference<VisualElement> _buttonsContainerReference = new();

    private readonly Subject<(SceneNavigationType Type, int Index)> _navigationRequested = new();
    private readonly CompositeDisposable _buttonDisposables = new();

    private List<SceneSwitchButton> _buttons;

    public Observable<(SceneNavigationType Type, int Index)> NavigationRequested => _navigationRequested;

    protected override bool IsVisualReady => _buttons != null;

    private void Awake()
    {
        _buttonsContainerReference.RegisterReferenceResolvedCallback(OnContainerResolved);
        _buttonsContainerReference.RegisterReferenceUnloadedCallback(OnContainerUnloaded);
    }

    protected override void OnDestroy()
    {
        _buttonDisposables.Dispose();
        _navigationRequested.Dispose();

        _buttonsContainerReference.UnregisterReferenceResolvedCallback(OnContainerResolved);
        _buttonsContainerReference.UnregisterReferenceUnloadedCallback(OnContainerUnloaded);

        base.OnDestroy();
    }

    public void UpdateButtonEnabled(int firstLevel, int currentScene, int achievedLevel)
    {
        foreach (SceneSwitchButton button in _buttons)
        {
            bool enabledSelf = button.NavigationType switch
            {
                SceneNavigationType.SpecificIndex => button.SpecificIndex <= achievedLevel,
                SceneNavigationType.PreviousLevel => currentScene > firstLevel,
                SceneNavigationType.NextLevel => currentScene < achievedLevel,
                _ => true
            };

            button.enabledSelf = enabledSelf;
        }
    }

    private void OnContainerResolved(VisualElement container)
    {
        _buttonDisposables.Clear();
        _buttons = container.Query<SceneSwitchButton>().ToList();

        foreach (SceneSwitchButton button in _buttons)
        {
            button.ClickedAsObservable()
                .Subscribe(_ => _navigationRequested.OnNext((button.NavigationType, button.SpecificIndex)))
                .AddTo(_buttonDisposables);
        }

        RefreshEnabledState();
    }

    private void OnContainerUnloaded(VisualElement container)
    {
        _buttonDisposables.Clear();
        _buttons = null;
        RefreshEnabledState();
    }
}
