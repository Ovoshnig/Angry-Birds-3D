using Ovoshnig.ObjectCollision.Collision;
using Ovoshnig.ObjectDestruction.Entity;

namespace Ovoshnig.ObjectDestruction.Destruction;

public readonly record struct DamageData(DestructibleEntityView EntityView, CollisionType CollisionType,
    float DamageAmount);
