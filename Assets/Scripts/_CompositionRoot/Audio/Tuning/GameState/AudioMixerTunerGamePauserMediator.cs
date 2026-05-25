using R3;

public class AudioMixerTunerGamePauserMediator : Mediator
{
    private readonly AudioMixerTuner _audioMixerTuner;
    private readonly GamePauser _gamePauser;

    public AudioMixerTunerGamePauserMediator(AudioMixerTuner audioMixerTuner, GamePauser gamePauser)
    {
        _audioMixerTuner = audioMixerTuner;
        _gamePauser = gamePauser;
    }

    protected override void Bind(CompositeDisposable disposables)
    {
        _gamePauser.IsPaused
            .Subscribe(_audioMixerTuner.SetPause)
            .AddTo(disposables);
    }
}
