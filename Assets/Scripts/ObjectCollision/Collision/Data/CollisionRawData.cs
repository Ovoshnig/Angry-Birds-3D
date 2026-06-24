using UnityEngine;

public record CollisionRawData(Vector3 ContactNormal, Vector3 RelativeVelocity, float ImpulseMagnitude, int ContactCount);
