using UnityEngine;

public class SceneButtonView : ButtonView
{
    [field: SerializeField] public SceneNavigationType NavigationType { get; private set; }
    [field: SerializeField] public int SpecificIndex { get; private set; }

#if UNITY_EDITOR
    public void SetSpecificIndex(int index)
    {
        if (UnityEditor.EditorApplication.isPlaying)
            return;

        NavigationType = SceneNavigationType.SpecificIndex;
        SpecificIndex = index;
    }
#endif
}
