using Ovoshnig.ObjectCollision.Entity;
using UnityEngine;

namespace Ovoshnig.ObjectCollision.Collision;

public readonly record struct CollisionData(CollidableEntityView EntityView, CollisionType Type,
    Vector3 Point, float Force);
