using AngryBirds3D.LevelScore.Points;
using AngryBirds3D.LevelScore.RatingEvaluation;
using AngryBirds3D.LevelScore.RecordRating;
using AngryBirds3D.LevelScore.Score;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace AngryBirds3D.LevelScore
{
    [Serializable]
    public class LevelScoreInstaller : IInstaller
    {
        [SerializeField] private ScoreInstaller _scoreInstaller;
        [SerializeField] private PointsInstaller _pointsInstaller;
        [SerializeField] private RatingEvaluationInstaller _ratingEvaluationInstaller;
        [SerializeField] private RecordRatingInstaller _recordRatingInstaller;

        public void Install(IContainerBuilder builder)
        {
            _scoreInstaller.Install(builder);
            _pointsInstaller.Install(builder);
            _ratingEvaluationInstaller.Install(builder);
            _recordRatingInstaller.Install(builder);
        }
    }
}
