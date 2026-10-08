using AngryBirds3D.Bird.Animation;
using AngryBirds3D.Bird.Destruction;
using AngryBirds3D.Bird.Flight;
using AngryBirds3D.Bird.Power;
using AngryBirds3D.LevelScore.Points;
using Ovoshnig.ObjectCollision.Entity;
using UnityEngine;

namespace AngryBirds3D.Bird.Entity
{
    [RequireComponent(typeof(BirdFlyerView))]
    [RequireComponent(typeof(BirdDestroyerView))]
    [RequireComponent(typeof(BirdPowerView))]
    [RequireComponent(typeof(BirdAnimatorView))]
    public class BirdEntityView : CollidableEntityView
    {
        [field: SerializeField] public BirdSfxProfile SfxProfile { get; private set; }
        [field: SerializeField] public PointsSettings PointsSettings { get; private set; }
        [field: SerializeField] public Color FeatherColor { get; private set; }

        public BirdFlyerView FlyerView { get; private set; }
        public BirdDestroyerView DestroyerView { get; private set; }
        public BirdPowerView PowerView { get; private set; }
        public BirdAnimatorView AnimatorView { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            FlyerView = GetComponent<BirdFlyerView>();
            DestroyerView = GetComponent<BirdDestroyerView>();
            PowerView = GetComponent<BirdPowerView>();
            AnimatorView = GetComponent<BirdAnimatorView>();
        }
    }
}
