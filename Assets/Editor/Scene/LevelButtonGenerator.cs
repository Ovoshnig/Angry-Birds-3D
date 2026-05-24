using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(LevelButtonGeneratorView))]
public class LevelButtonGenerator : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!GUILayout.Button("Generate"))
            return;

        if (Application.isPlaying)
        {
            Debug.LogWarning("Generation is disabled during Play Mode.");
            return;
        }

        Generate((LevelButtonGeneratorView)target);
    }

    private static void Generate(LevelButtonGeneratorView generatorView)
    {
        RectTransform parent = generatorView.LevelButtonParent;

        if (parent == null)
        {
            Debug.LogError("Level button parent is not assigned.", generatorView);
            return;
        }

        Undo.IncrementCurrentGroup();
        int undoGroup = Undo.GetCurrentGroup();
        Undo.SetCurrentGroupName("Generate Level Buttons");

        Undo.RegisterFullObjectHierarchyUndo(parent.gameObject, "Generate Level Buttons");

        if (!TryDestroyOld(parent))
        {
            Undo.RevertAllDownToGroup(undoGroup);
            return;
        }

        SceneSettings sceneSettings = generatorView.GameSettings.SceneSettings;

        for (int i = 0; i < sceneSettings.LevelCount; i++)
        {
            SceneButtonView levelView = (SceneButtonView)PrefabUtility.InstantiatePrefab(
                generatorView.LevelButtonPrefab,
                parent);

            levelView.name = $"LevelButton{i + 1}";
            Undo.RegisterCreatedObjectUndo(levelView.gameObject, "Generate Level Buttons");

            int sceneIndex = sceneSettings.FirstLevelIndex + i;
            int displayedLevelIndex = i + 1;

            Undo.RecordObject(levelView, "Generate Level Buttons");
            levelView.SetSpecificIndex(sceneIndex);

            LevelIndexView indexView = levelView.GetComponentInChildren<LevelIndexView>();
            Undo.RecordObject(indexView, "Generate Level Buttons");
            indexView.SetIndex(displayedLevelIndex);
        }

        EditorUtility.SetDirty(parent);
        EditorSceneManager.MarkSceneDirty(generatorView.gameObject.scene);
        Undo.CollapseUndoOperations(undoGroup);
    }

    private static bool TryDestroyOld(RectTransform parent)
    {
        IEnumerable<RectTransform> children = parent.Cast<RectTransform>();
        RectTransform invalidChild = children.FirstOrDefault(c => c.GetComponent<SceneButtonView>() == null);

        if (invalidChild != null)
        {
            Debug.LogError($"Invalid child object {invalidChild.name}, cancelling generation.", invalidChild);
            return false;
        }

        for (int i = parent.childCount - 1; i >= 0; i--)
            Undo.DestroyObjectImmediate(parent.GetChild(i).gameObject);

        return true;
    }
}
