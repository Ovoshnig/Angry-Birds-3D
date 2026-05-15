using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class LevelIndexView : UIView
{
    private TMP_Text _text;

    private void Awake() => _text = GetComponent<TMP_Text>();

    public void SetIndex(int season, int level) => _text.SetText("{0}-{1}", season, level);
}
