using UnityEngine;

namespace Ovoshnig.ObjectCollision.Collision;

public readonly record struct CollisionRawData(Vector3 ContactNormal, Vector3 RelativeVelocity,
    float ImpulseMagnitude, int ContactCount);
