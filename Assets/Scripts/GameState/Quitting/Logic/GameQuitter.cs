using UnityEngine;

namespace Ovoshnig.GameState.Quitting
{
    public class GameQuitter
    {
        public void Quit(int exitCode = 0)
        {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit(exitCode);
    #endif
        }
    }
}
