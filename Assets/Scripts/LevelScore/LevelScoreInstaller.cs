using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class LevelScoreInstaller : IInstaller
{
    [SerializeField] private ScoreInstaller _scoreInstaller;
    [SerializeField] private PointsInstaller _pointsInstaller;
    [SerializeField] private RatingEvaluationInstaller _ratingEvaluationInstaller;

    public void Install(IContainerBuilder builder)
    {
        _scoreInstaller.Install(builder);
        _pointsInstaller.Install(builder);
        _ratingEvaluationInstaller.Install(builder);
    }
}
