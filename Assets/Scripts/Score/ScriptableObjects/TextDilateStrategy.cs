using DG.Tweening;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/DOTween/Text Dilate Strategy", 
    fileName = nameof(TextDilateStrategy))]
public class TextDilateStrategy : TextAnimationStrategy
{
    public override void ResetState(TMP_Text text, Material material)
    {
        text.transform.localScale = Vector3.one;
        material.SetFloat(ShaderUtilities.ID_FaceDilate, -1f);
    }

    public override Sequence CreateSequence(TMP_Text text, Material material)
    {
        return DOTween.Sequence()
            .Append(material.DOFloat(0f, ShaderUtilities.ID_FaceDilate, InDuration).SetEase(InEase))
            .AppendInterval(StayDuration)
            .Append(material.DOFloat(-1f, ShaderUtilities.ID_FaceDilate, OutDuration).SetEase(OutEase));
    }
}
