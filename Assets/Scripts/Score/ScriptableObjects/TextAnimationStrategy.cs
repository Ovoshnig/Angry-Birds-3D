using DG.Tweening;
using TMPro;
using UnityEngine;

public abstract class TextAnimationStrategy : ScriptableObject
{
    [field: SerializeField, Min(0.1f)] protected float InDuration { get; private set; } = 0.5f;
    [field: SerializeField, Min(0.1f)] protected float StayDuration { get; private set; } = 0.5f;
    [field: SerializeField, Min(0.1f)] protected float OutDuration { get; private set; } = 0.5f;
    [field: SerializeField] protected Ease InEase { get; private set; } = Ease.OutExpo;
    [field: SerializeField] protected Ease OutEase { get; private set; } = Ease.InExpo;

    public abstract Sequence CreateSequence(TMP_Text text, Material material);

    public abstract void ResetState(TMP_Text text, Material material);
}
