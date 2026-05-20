public record DamageEvent(DestructibleEntityView EntityView, ObjectDestroyerView DestroyerView,
    CollisionType CollisionType, float DamageAmount);
