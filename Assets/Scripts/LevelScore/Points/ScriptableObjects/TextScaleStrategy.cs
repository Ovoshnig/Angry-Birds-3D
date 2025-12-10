using DG.Tweening;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/DOTween/Text Scale Strategy", 
    fileName = nameof(TextScaleStrategy))]
public class TextScaleStrategy : TextAnimationStrategy
{
    public override void ResetState(TMP_Text text, Material material)
    {
        material.SetFloat(ShaderUtilities.ID_FaceDilate, 0f);
        text.transform.localScale = Vector3.zero;
    }

    public override Sequence CreateSequence(TMP_Text text, Material material)
    {
        return DOTween.Sequence()
            .Append(text.transform.DOScale(Vector3.one, InDuration).SetEase(InEase))
            .AppendInterval(StayDuration)
            .Append(text.transform.DOScale(Vector3.zero, OutDuration).SetEase(OutEase));
    }
}
