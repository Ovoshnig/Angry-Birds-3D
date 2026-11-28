using Cysharp.Threading.Tasks;
using Ovoshnig.Screen.Input;
using R3;
using System;
using System.Threading;
using VContainer.Unity;
using UnitySplashScreen = UnityEngine.Rendering.SplashScreen;

namespace Ovoshnig.Screen.SplashScreen;

public class SplashScreenDisplayer(ScreenInputProvider screenInputProvider) : IStartable, IDisposable
{
    private readonly ScreenInputProvider _screenInputProvider = screenInputProvider;
    private readonly ReactiveProperty<bool> _isPlaying = new(false);
    private readonly CancellationTokenSource _cts = new();
    private readonly CompositeDisposable _disposables = [];

    public ReadOnlyReactiveProperty<bool> IsPlaying => _isPlaying;

    public void Start()
    {
        _screenInputProvider.SkipSplashImagePressed
            .Where(isPressed => isPressed)
            .Take(1)
            .Subscribe(_ => Stop())
            .AddTo(_disposables);

        DisplayAsync().Forget();
    }

    public void Dispose()
    {
        Stop();

        _disposables.Dispose();
        _isPlaying.Dispose();

        _cts.Cancel();
        _cts.Dispose();
    }

    private static void Stop()
    {
        if (!UnitySplashScreen.isFinished)
            UnitySplashScreen.Stop(UnitySplashScreen.StopBehavior.FadeOut);
    }

    private async UniTask DisplayAsync()
    {
        UnitySplashScreen.Begin();
        UnitySplashScreen.Draw();
        _isPlaying.Value = true;

        await UniTask.WaitUntil(() => UnitySplashScreen.isFinished, cancellationToken: _cts.Token);

        _isPlaying.Value = false;
    }
}
