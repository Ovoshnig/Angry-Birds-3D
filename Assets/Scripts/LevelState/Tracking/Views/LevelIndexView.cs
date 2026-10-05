using Ovoshnig.Scene.Switching;
using Ovoshnig.UI.Basic;
using TMPro;
using UnityEngine;

namespace Ovoshnig.LevelState.Tracking
{
    [RequireComponent(typeof(TMP_Text))]
    public class LevelIndexView : UIView
    {
        private TMP_Text _text;

        private void Awake() => _text = GetComponent<TMP_Text>();

        public void SetIndex(int season, int level) => _text.SetText($"{season}-{level}");
    }
}
