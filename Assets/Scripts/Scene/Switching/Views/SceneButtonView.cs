using UnityEngine;

public class SceneButtonView : ButtonView
{
    [field: SerializeField] public SceneNavigationType NavigationType { get; private set; }
    [field: SerializeField] public int SpecificIndex { get; private set; }
}
