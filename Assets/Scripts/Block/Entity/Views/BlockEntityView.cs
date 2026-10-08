using AngryBirds3D.Block.Destruction;
using AngryBirds3D.Block.Particle;
using Ovoshnig.ObjectDestruction.Entity;
using UnityEngine;

namespace AngryBirds3D.Block.Entity
{
    [RequireComponent(typeof(BlockDestroyerView))]
    public class BlockEntityView : DestructibleEntityView
    {
        [field: SerializeField] public BlockParticleProfile ParticleProfile { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            DestroyerView = GetComponent<BlockDestroyerView>();
        }
    }
}
