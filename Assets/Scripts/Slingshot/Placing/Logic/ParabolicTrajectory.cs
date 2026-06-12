public static class ParabolicTrajectory
{
    private const float HeightMultiplier = 4f;

    public static float CalculateOffset(float height, float progress) =>
        HeightMultiplier * height * progress * (1f - progress);
}
