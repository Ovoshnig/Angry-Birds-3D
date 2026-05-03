using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

public class SceneSwitch : IInitializable, IDisposable
{
    public enum SceneType
    {
        MainMenu,
        GameLevel,
        Credits
    }

    private readonly ReactiveProperty<bool> _isSceneLoading = new(true);
    private readonly CancellationTokenSource _cts = new();

    private int _currentLevel;

    public ReadOnlyReactiveProperty<bool> IsSceneLoading => _isSceneLoading;

    public void Initialize()
    {
        _currentLevel = SceneManager.GetActiveScene().buildIndex;

        WaitForFirstSceneLoadAsync().Forget();
    }

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();

        _isSceneLoading.Dispose();
    }

    public UniTask LoadCurrentLevelAsync() => LoadLevelAsync(_currentLevel);

    public UniTask LoadNextLevelAsync() => LoadLevelAsync(_currentLevel + 1);

    public async UniTask LoadLevelAsync(int index)
    {
        if (!IsValidIndex(index))
            return;

        try
        {
            _isSceneLoading.Value = true;

            await SceneManager.LoadSceneAsync(index).ToUniTask(cancellationToken: _cts.Token);

            _currentLevel = index;
            _isSceneLoading.Value = false;
        }
        catch (OperationCanceledException)
        {
        }
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
