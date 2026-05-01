using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;
using UnityEngine.Rendering;
using VContainer.Unity;

public class SplashScreenDisplayer : IStartable, IDisposable
{
    private readonly ScreenInputHandler _screenInputHandler;
    private readonly ReactiveProperty<bool> _isPlaying = new(false);
    private readonly CancellationTokenSource _cts = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public SplashScreenDisplayer(ScreenInputHandler screenInputHandler) =>
        _screenInputHandler = screenInputHandler;

    public ReadOnlyReactiveProperty<bool> IsPlaying => _isPlaying;

    public void Start()
    {
        _screenInputHandler.SkipSplashImagePressed
            .Where(isPressed => isPressed)
            .Take(1)
            .Subscribe(_ => Stop())
            .AddTo(_compositeDisposable);

        DisplayAsync().Forget();
    }

    public void Dispose()
    {
        Stop();

        _compositeDisposable.Dispose();
        _isPlaying.Dispose();

        _cts.Cancel();
        _cts.Dispose();
    }

    private async UniTask DisplayAsync()
    {
        SplashScreen.Begin();
        SplashScreen.Draw();
        _isPlaying.Value = true;

        await UniTask.WaitUntil(() => SplashScreen.isFinished, cancellationToken: _cts.Token)
            .SuppressCancellationThrow();

        _isPlaying.Value = false;
    }

    private void Stop()
    {
        if (!SplashScreen.isFinished)
            SplashScreen.Stop(SplashScreen.StopBehavior.FadeOut);
    }
}
