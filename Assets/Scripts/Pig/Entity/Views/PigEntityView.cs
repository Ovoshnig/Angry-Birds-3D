using AngryBirds3D.Pig.Destruction;
using Ovoshnig.ObjectDestruction.Entity;
using UnityEngine;

namespace AngryBirds3D.Pig.Entity
{
    [RequireComponent(typeof(PigDestroyerView))]
    public class PigEntityView : DestructibleEntityView
    {
        protected override void Awake()
        {
            base.Awake();
            DestroyerView = GetComponent<PigDestroyerView>();
        }
    }
}
