using UnityEngine.UIElements;

namespace Ovoshnig.Scene.Switching
{
    [UxmlElement]
    public partial class SceneSwitchButton : Button
    {
        [UxmlAttribute]
        public SceneNavigationType NavigationType { get; private set; }

        [UxmlAttribute]
        public int SpecificIndex { get; private set; }
    }
}
