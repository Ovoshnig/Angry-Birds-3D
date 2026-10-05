using Ovoshnig.UI.Basic;
using UnityEngine;

namespace Ovoshnig.GameState.SocialLink
{
    public class SocialLinkButtonView : ButtonView
    {
        [field: SerializeField] public string Url { get; private set; }
    }
}
