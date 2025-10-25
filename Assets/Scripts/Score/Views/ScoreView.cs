using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ScoreView : MonoBehaviour
{
    private TMP_Text _text = null;

    public TMP_Text Text
    {
        get
        {
            if (_text == null)
                _text = GetComponent<TMP_Text>();

            return _text;
        }
    }

    public void SetScore(int value) => Text.text = $"SCORE\n{value:D4}";
}
