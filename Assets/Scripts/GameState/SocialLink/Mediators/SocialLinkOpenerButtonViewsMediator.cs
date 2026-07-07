using R3;
using System.Collections.Generic;

public class SocialLinkOpenerButtonViewsMediator : UIViewsMediator<SocialLinkButtonView>
{
    private readonly SocialLinkOpener _socialLinkOpener;

    public SocialLinkOpenerButtonViewsMediator(SocialLinkOpener socialLinkOpener,
        IReadOnlyList<SocialLinkButtonView> views) : base(views) => _socialLinkOpener = socialLinkOpener;

    protected override void OnViewEnabled(SocialLinkButtonView view, CompositeDisposable viewDisposables)
    {
        view.Clicked
            .Subscribe(_ => _socialLinkOpener.Open(view.Url))
            .AddTo(viewDisposables);
    }
}
