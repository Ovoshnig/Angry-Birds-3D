using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class RatingShowingInstaller : IInstaller
{
    [SerializeField] private RectTransform _ratingShowerParent;

    public void Install(IContainerBuilder builder)
    {
        IReadOnlyList<RatingShowerView> views = _ratingShowerParent.GetComponentsInChildren<RatingShowerView>(true);
        builder.RegisterInstance(views);

        builder.UseEntryPoints(entryPoints =>
        {
            entryPoints.Add<RatingShower>().AsSelf();
            entryPoints.Add<RatingShowerViewsMediator>();
        });
    }
}
