using Ovoshnig.Audio.Tuning;
using Ovoshnig.GameState.Pause;
using Ovoshnig.Mediation;
using R3;

namespace AngryBirds3D.Composition;

public class AudioMixerTunerGamePauserMediator(AudioMixerTuner audioMixerTuner, GamePauser gamePauser)
    : Mediator
{
    private readonly AudioMixerTuner _audioMixerTuner = audioMixerTuner;
    private readonly GamePauser _gamePauser = gamePauser;

    protected override void Bind(CompositeDisposable disposables)
    {
        _gamePauser.IsPaused
            .Subscribe(_audioMixerTuner.SetPause)
            .AddTo(disposables);
    }
}
