using Ovoshnig.ObjectCollision.Collision;
using Ovoshnig.ObjectDestruction.Entity;

namespace Ovoshnig.ObjectDestruction.Destruction
{
    public record DamageData(DestructibleEntityView EntityView, CollisionType CollisionType, float DamageAmount);
}
