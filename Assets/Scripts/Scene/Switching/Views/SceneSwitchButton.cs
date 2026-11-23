using UnityEngine.UIElements;

namespace Ovoshnig.Scene.Switching
{
    [UxmlElement]
    public partial class SceneSwitchButton : Button
    {
        [UxmlAttribute]
        public SceneNavigationType NavigationType { get; set; }

        [UxmlAttribute]
        public int SpecificIndex { get; set; }
    }
}
