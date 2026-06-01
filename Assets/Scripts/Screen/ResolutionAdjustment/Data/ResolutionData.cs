using System;
using UnityEngine;

public record ResolutionData(int Width, int Height, RefreshRate RefreshRate) : IComparable<ResolutionData>
{
    public int CompareTo(ResolutionData other)
    {
        int widthComparison = Width.CompareTo(other.Width);

        if (widthComparison != 0)
            return widthComparison;

        int heightComparison = Height.CompareTo(other.Height);

        if (heightComparison != 0)
            return heightComparison;

        return RefreshRate.value.CompareTo(other.RefreshRate.value);
    }

    public override string ToString() => $"{Width}x{Height}@{RefreshRate.value:F2}";
}
