using R3;

public class SaveStorageAchievedViewMediator : Mediator
{
    private readonly SaveStorage _saveStorage;
    private readonly AchievedLevelButtonView _achievedLevelButtonView;

    public SaveStorageAchievedViewMediator(SaveStorage saveStorage, 
        AchievedLevelButtonView achievedLevelButtonView)
    {
        _saveStorage = saveStorage;
        _achievedLevelButtonView = achievedLevelButtonView;
    }

    public override void Start()
    {
        Observable
            .EveryUpdate(_achievedLevelButtonView.destroyCancellationToken)
            .Where(_ => _achievedLevelButtonView.isActiveAndEnabled)
            .Subscribe(_ =>
            {
                bool saveCreated = _saveStorage.Get(SaveConstants.SaveCreatedKey, false);
                _achievedLevelButtonView.SetInteractable(saveCreated);
            })
            .AddTo(CompositeDisposable);
    }
}
