using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class FinalScoreView : UIView
{
    private TMP_Text _text;

    private void Awake() => _text = GetComponent<TMP_Text>();

    public void SetScore(int value) => _text.SetText("Score:\n{0:0000}", value);
}
