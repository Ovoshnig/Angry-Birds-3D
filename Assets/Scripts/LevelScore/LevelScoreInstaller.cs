using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class LevelScoreInstaller : IInstaller
{
    [SerializeField] private ScoreInstaller _scoreInstaller;
    [SerializeField] private PointsInstaller _pointsInstaller;

    public void Install(IContainerBuilder builder)
    {
        _scoreInstaller.Install(builder);
        _pointsInstaller.Install(builder);
    }
}
