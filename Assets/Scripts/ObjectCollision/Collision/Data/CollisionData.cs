using UnityEngine;

public record CollisionData(CollidableEntityView EntityView, CollisionType Type, Vector3 Point, float Force);
