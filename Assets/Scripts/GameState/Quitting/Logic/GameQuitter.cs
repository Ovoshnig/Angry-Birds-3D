using UnityEngine;

namespace Ovoshnig.GameState.Quitting;

public static class GameQuitter
{
    public static void Quit(int exitCode = 0)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(exitCode);
#endif
    }
}
