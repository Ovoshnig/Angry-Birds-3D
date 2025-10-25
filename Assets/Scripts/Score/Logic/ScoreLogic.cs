using R3;
using System;
using VContainer.Unity;

public class ScoreLogic : IInitializable, IDisposable
{
    private readonly ScoreModel _scoreModel;
    private readonly ReactiveProperty<int> _lastPoints = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public ScoreLogic(ScoreModel scoreModel) => _scoreModel = scoreModel;

    public ReadOnlyReactiveProperty<int> LastPoints => _lastPoints;

    public void Initialize()
    {
        _lastPoints
            .Subscribe(points => _scoreModel.Increase(points))
            .AddTo(_compositeDisposable);
    }

    public void Dispose() => _compositeDisposable.Dispose();
}
