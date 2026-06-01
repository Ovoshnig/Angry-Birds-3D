using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RatingShowerView : UIView
{
    private Image[] _images;

    [field: SerializeField] public int LevelIndex { get; private set; }

    private void Awake() => _images = GetComponentsInChildren<Image>()
        .Where(i => i.transform != transform)
        .ToArray();

    public void SetStarCount(int count)
    {
        foreach (var image in _images)
            image.enabled = false;

        for (int i = 0; i < count; i++)
            _images[i].enabled = true;
    }

#if UNITY_EDITOR
    public void SetLevelIndex(int index)
    {
        if (UnityEditor.EditorApplication.isPlaying)
            return;

        LevelIndex = index;
    }
#endif
}
