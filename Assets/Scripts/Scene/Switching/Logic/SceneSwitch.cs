using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

public class SceneSwitch : IInitializable, IDisposable
{
    private readonly SceneSettings _sceneSettings;
    private readonly ReactiveProperty<bool> _isSceneLoading = new(true);
    private readonly CancellationTokenSource _cts = new();

    private int _currentLevelIndex;

    public SceneSwitch(SceneSettings sceneSettings) => _sceneSettings = sceneSettings;

    public ReadOnlyReactiveProperty<bool> IsSceneLoading => _isSceneLoading;

    public void Initialize()
    {
        _currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        WaitForFirstSceneLoadAsync().Forget();
    }

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();

        _isSceneLoading.Dispose();
    }

    public async UniTask LoadSceneAsync(SceneNavigationType navigationType, int specificIndex = -1)
    {
        int index = navigationType switch
        {
            SceneNavigationType.MainMenu => _sceneSettings.MainMenuIndex,
            SceneNavigationType.FirstLevel => _sceneSettings.FirstLevelIndex,
            SceneNavigationType.CurrentLevel => _currentLevelIndex,
            SceneNavigationType.NextLevel => _currentLevelIndex + 1,
            SceneNavigationType.SpecificIndex => specificIndex,
            _ => throw new ArgumentOutOfRangeException(nameof(navigationType))
        };

        if (!IsValidIndex(index))
            return;

        _isSceneLoading.Value = true;

        await SceneManager.LoadSceneAsync(index).ToUniTask(cancellationToken: _cts.Token);

        _currentLevelIndex = index;
        _isSceneLoading.Value = false;
    }

    private async UniTask WaitForFirstSceneLoadAsync()
    {
        await UniTask.WaitUntil(() =>
        SceneManager.GetActiveScene().isLoaded, cancellationToken: _cts.Token);

        _isSceneLoading.Value = false;
    }

    private bool IsValidIndex(int index)
    {
        if (index >= 0 && index < SceneManager.sceneCountInBuildSettings)
            return true;

        Debug.LogWarning($"Cannot load scene with index {index}, it is out of scene list bounds.");
        return false;
    }
}
