using UnityEngine;

public record CollisionData(Vector3 ContactNormal, Vector3 RelativeVelocity, float ImpulseMagnitude, int ContactCount);
