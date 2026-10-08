using Ovoshnig.Cursor.Showing;
using Ovoshnig.Cursor.State;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Ovoshnig.Cursor
{
    [Serializable]
    public class CursorInstaller : IInstaller
    {
        [SerializeField] private CursorShowingInstaller _showingInstaller;
        [SerializeField] private CursorStateInstaller _stateInstaller;

        public void Install(IContainerBuilder builder)
        {
            _showingInstaller.Install(builder);
            _stateInstaller.Install(builder);
        }
    }
}
