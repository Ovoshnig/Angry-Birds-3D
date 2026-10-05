using AngryBirds3D.LevelState.Tracking;
using Ovoshnig.Cursor.State;
using Ovoshnig.Mediation;
using R3;
using System.Collections.Generic;

namespace AngryBirds3D.Composition
{
    public class CursorStateModelCompletionPanelViewsMediator : Mediator
    {
        private readonly CursorStateModel _cursorStateModel;
        private readonly IReadOnlyList<CompletionPanelView> _completionPanelViews;
    
        public CursorStateModelCompletionPanelViewsMediator(CursorStateModel cursorStateModel,
            IReadOnlyList<CompletionPanelView> completionPanelViews)
        {
            _cursorStateModel = cursorStateModel;
            _completionPanelViews = completionPanelViews;
        }
    
        protected override void Bind(CompositeDisposable disposables)
        {
            foreach (var panelView in _completionPanelViews)
            {
                panelView.Shown
                    .Subscribe(_ => _cursorStateModel.SetState(CursorState.UIHover))
                    .AddTo(disposables);
            }
        }
    }
}
