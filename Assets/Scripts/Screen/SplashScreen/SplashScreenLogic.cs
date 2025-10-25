using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;
using UnityEngine.Rendering;
using VContainer.Unity;

public class SplashScreenLogic : IInitializable, IDisposable
{
    private readonly ScreenInputHandler _screenInputHandler;
    private readonly ReactiveProperty<bool> _isPlaying = new(false);
    private readonly CancellationTokenSource _cts = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public SplashScreenLogic(ScreenInputHandler screenInputHandler) =>
        _screenInputHandler = screenInputHandler;

    public ReadOnlyReactiveProperty<bool> IsPlaying => _isPlaying;

    public void Initialize()
    {
        PlayAsync().Forget();

        _screenInputHandler.SkipSplashImagePressed
            .Where(isPressed => isPressed)
            .Subscribe(_ => Stop())
            .AddTo(_compositeDisposable);
    }

    public void Dispose()
    {
        Stop();

        if (!_compositeDisposable.IsDisposed)
            _compositeDisposable.Dispose();

        _cts.Cancel();
        _cts.Dispose();
    }

    private async UniTask PlayAsync()
    {
        SplashScreen.Begin();
        SplashScreen.Draw();
        _isPlaying.Value = true;

        try
        {
            await UniTask.WaitUntil(() => SplashScreen.isFinished, cancellationToken: _cts.Token);
        }
        finally
        {
            _isPlaying.Value = false;
        }
    }

    private void Stop()
    {
        if (!SplashScreen.isFinished)
        {
            SplashScreen.Stop(SplashScreen.StopBehavior.FadeOut);
            _compositeDisposable.Dispose();
        }
    }
}
