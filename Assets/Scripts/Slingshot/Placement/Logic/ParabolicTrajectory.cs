using UnityEngine;

public static class ParabolicTrajectory
{
    private const float OffsetMultiplier = 4f;
    private const float PeakProgressDivider = 8f;

    public static float CalculateOffset(float height, float progress)
        => OffsetMultiplier * height * progress * (1f - progress);

    public static float CalculatePeakProgress(float startY, float endY, float jumpHeight)
    {
        if (jumpHeight <= 0f)
            return 0.5f;

        float progress = 0.5f + (endY - startY) / (PeakProgressDivider * jumpHeight);
        return Mathf.Clamp01(progress);
    }
}
