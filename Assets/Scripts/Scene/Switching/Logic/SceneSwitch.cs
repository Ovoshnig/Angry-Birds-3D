using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

public class SceneSwitch : IStartable, IDisposable
{
    private readonly SceneSettings _sceneSettings;
    private readonly ReactiveProperty<bool> _isSceneLoading = new(false);
    private readonly ReactiveProperty<float> _loadingProgress = new(1f);
    private readonly CancellationTokenSource _cts = new();

    private int _currentSceneIndex;

    public SceneSwitch(SceneSettings sceneSettings) => _sceneSettings = sceneSettings;

    public ReadOnlyReactiveProperty<bool> IsSceneLoading => _isSceneLoading;
    public ReadOnlyReactiveProperty<float> LoadingProgress => _loadingProgress;

    public void Start() => _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();

        _isSceneLoading.Dispose();
        _loadingProgress.Dispose();
    }

    public async UniTask LoadSceneAsync(SceneNavigationType navigationType, int specificIndex = -1)
    {
        int index = navigationType switch
        {
            SceneNavigationType.MainMenu => _sceneSettings.MainMenuIndex,
            SceneNavigationType.FirstLevel => _sceneSettings.FirstLevelIndex,
            SceneNavigationType.PreviousLevel => _currentSceneIndex - 1,
            SceneNavigationType.CurrentLevel => _currentSceneIndex,
            SceneNavigationType.NextLevel => _currentSceneIndex + 1,
            SceneNavigationType.SpecificIndex => specificIndex,
            _ => throw new ArgumentOutOfRangeException(nameof(navigationType))
        };

        if (!IsValidIndex(index))
            return;

        _isSceneLoading.Value = true;

        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        operation.allowSceneActivation = false;

        float currentProgress = SceneSwitchingConstants.ProgressMin;

        while (!operation.isDone)
        {
            float targetProgress = operation.progress / SceneSwitchingConstants.UnityMaxLoadingProgress;

            currentProgress = Mathf.Lerp(currentProgress, targetProgress,
                Time.unscaledDeltaTime * _sceneSettings.InterpolationSpeed);
            _loadingProgress.Value = currentProgress;

            if (SceneSwitchingConstants.ProgressMax - currentProgress < SceneSwitchingConstants.ProgressCompletionThreshold)
            {
                _loadingProgress.Value = SceneSwitchingConstants.ProgressMax;
                operation.allowSceneActivation = true;
            }

            await UniTask.Yield(_cts.Token);
        }

        _currentSceneIndex = index;
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
