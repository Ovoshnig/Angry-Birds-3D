using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;
using VContainer.Unity;

public class StartCameraSwitch : IStartable, IDisposable
{
    private readonly CameraSwitchView _switchView;
    private readonly CameraSettings _settings;
    private readonly Subject<Unit> _completed = new();
    private readonly CancellationTokenSource _cts = new();

    public StartCameraSwitch(CameraSwitchView switchView,
        CameraSettings settings)
    {
        _switchView = switchView;
        _settings = settings;
    }

    public Observable<Unit> Completed => _completed;

    public void Start() => SwitchAsync().Forget();

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();

        _completed.Dispose();
    }

    private async UniTask SwitchAsync()
    {
        await _switchView.SwitchToStructureAsync();
        await UniTask.WaitForSeconds(_settings.StructureShowingDuration, cancellationToken: _cts.Token);

        await _switchView.SwitchToSlingshotAsync();
        await UniTask.WaitForSeconds(_settings.SlingshotShowingDuration, cancellationToken: _cts.Token);

        await _switchView.SwitchToGeneralAsync();

        _completed.OnNext(Unit.Default);
    }
}
