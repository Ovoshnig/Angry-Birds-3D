using Ovoshnig.UI.Basic;
using R3;
using System.Collections.Generic;
using UnityEngine;

namespace Ovoshnig.GameState.SocialLink;

public class SocialLinkOpenerButtonViewsMediator(IReadOnlyList<SocialLinkButtonView> views)
    : UIViewsMediator<SocialLinkButtonView>(views)
{
    protected override void OnViewEnabled(SocialLinkButtonView view, CompositeDisposable viewDisposables)
    {
        view.Clicked
            .Subscribe(_ => Application.OpenURL(view.Url))
            .AddTo(viewDisposables);
    }
}
