using System;
using System.Collections.Frozen;
using System.IO;
using Unity.Scripting.LifecycleManagement;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Ovoshnig.Editor.Installer;

public static class InstallerGenerator
{
    [NoAutoStaticsCleanup]
    private static readonly FrozenSet<string> _ignoredFolders =
        new[] { "Scripts", "Assets", "Runtime", "Project", "Source" }
            .ToFrozenSet(StringComparer.OrdinalIgnoreCase);

    [MenuItem("Assets/Create/Scripting/Installer", false, priority = 3, secondaryPriority = 10f)]
    private static void Generate()
    {
        string directoryPath = GetSelectedPath();
        string installerName = GetInstallerName(directoryPath);
        string filePath = Path.Combine(directoryPath, $"{installerName}.cs");

        if (File.Exists(filePath))
            return;

        string code = directoryPath.Contains("_CompositionRoot")
            ? GetMediatorsInstallerTemplate(installerName)
            : GetFeatureInstallerTemplate(installerName);

        File.WriteAllText(filePath, code);
        AssetDatabase.Refresh();

        Object createdAsset = AssetDatabase.LoadAssetAtPath<Object>(filePath);

        if (createdAsset == null)
            return;

        Selection.activeObject = createdAsset;
    }

    private static string GetInstallerName(string path) =>
        path.Contains("_CompositionRoot")
            ? $"{GetFolders(path)[^1]}MediatorsInstaller"
            : GetFeatureInstallerName(GetFolders(path));

    private static string GetFeatureInstallerName(string[] folders) =>
        IsFolderIgnored(folders[^2])
            ? $"{folders[^1]}Installer"
            : $"{folders[^2]}{folders[^1]}Installer";

    private static bool IsFolderIgnored(string folderName) => _ignoredFolders.Contains(folderName);

    private static string[] GetFolders(string path) =>
        path.Split(['/', '\\'], StringSplitOptions.RemoveEmptyEntries);

    private static string GetSelectedPath()
    {
        string selectedPath = "Assets";

        foreach (Object assetObject in Selection.GetFiltered<Object>(SelectionMode.Assets))
        {
            selectedPath = AssetDatabase.GetAssetPath(assetObject);

            if (File.Exists(selectedPath))
                selectedPath = Path.GetDirectoryName(selectedPath);

            break;
        }

        return selectedPath;
    }

    private static string GetFeatureInstallerTemplate(string className) =>
        $$"""
        using System;
        using VContainer;
        using VContainer.Unity;

        namespace ;

        [Serializable]
        public class {{className}} : IInstaller
        {
            public void Install(IContainerBuilder builder)
            {
                throw new NotImplementedException();
            }
        }

        """;

    private static string GetMediatorsInstallerTemplate(string className) =>
        $$"""
        using VContainer;
        using VContainer.Unity;

        namespace AngryBirds3D.Composition;

        public class {{className}} : IInstaller
        {
            public void Install(IContainerBuilder builder)
            {
                throw new System.NotImplementedException();
            }
        }

        """;
}
