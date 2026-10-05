using UnityEngine;

namespace Ovoshnig.GameState.SocialLink
{
    public class SocialLinkOpener
    {
        public void Open(string url) => Application.OpenURL(url);
    }
}
