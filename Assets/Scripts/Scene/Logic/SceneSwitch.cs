using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;
using UnityEngine.SceneManagement;
using VContainer.Unity;

public class SceneSwitch : IInitializable, IDisposable
{
    private readonly ReactiveProperty<bool> _isSceneLoading = new(true);
    private readonly CancellationTokenSource _cts = new();

    private uint _currentLevel;

    public ReadOnlyReactiveProperty<bool> IsSceneLoading => _isSceneLoading.ToReadOnlyReactiveProperty();

    public void Initialize()
    {
        _currentLevel = (uint)SceneManager.GetActiveScene().buildIndex;

        WaitForFirstSceneLoadAsync().Forget();
    }

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }

    public async UniTask LoadCurrentLevelAsync() => await LoadLevelAsync(_currentLevel);

    public async UniTask LoadLevelAsync(uint index)
    {
        try
        {
            _isSceneLoading.Value = true;

            await SceneManager.LoadSceneAsync((int)index)
                .ToUniTask(cancellationToken: _cts.Token);

            _currentLevel = index;
            _isSceneLoading.Value = false;
        }
        catch (OperationCanceledException)
        {
            return;
        }
    }

    private async UniTask WaitForFirstSceneLoadAsync()
    {
        try
        {
            await UniTask.WaitUntil(() => SceneManager
                .GetActiveScene().isLoaded, cancellationToken: _cts.Token);

            _isSceneLoading.Value = false;
        }
        catch (OperationCanceledException)
        {
            return;
        }
    }
}
