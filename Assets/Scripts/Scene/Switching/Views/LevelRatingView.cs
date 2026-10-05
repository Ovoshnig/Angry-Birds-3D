using R3;
using UnityEngine;

namespace Ovoshnig.Scene.Switching
{
    public class LevelRatingView : MonoBehaviour
    {
        [SerializeField] private SceneSwitchButtonView _levelButtonView;

        private void Start()
        {
            _levelButtonView.IsInteractable
                .Subscribe(isInteractable => gameObject.SetActive(isInteractable))
                .AddTo(this);
        }
    }
}
