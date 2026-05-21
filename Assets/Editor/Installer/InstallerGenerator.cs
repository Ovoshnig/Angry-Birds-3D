using System;
using System.IO;
using UnityEditor;
using Object = UnityEngine.Object;

public static class InstallerGenerator
{
    [MenuItem("Assets/Create/Scripting/Installer", false, priority = 3, secondaryPriority = 10f)]
    public static void CreateInstaller() => Generate();

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

        UnityEngine.Object createdAsset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(filePath);

        if (createdAsset != null)
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

    private static bool IsFolderIgnored(string folderName) =>
        folderName.Equals("Scripts", StringComparison.OrdinalIgnoreCase) ||
        folderName.Equals("Assets", StringComparison.OrdinalIgnoreCase) ||
        folderName.Equals("Runtime", StringComparison.OrdinalIgnoreCase) ||
        folderName.Equals("Project", StringComparison.OrdinalIgnoreCase) ||
        folderName.Equals("Source", StringComparison.OrdinalIgnoreCase);

    private static string[] GetFolders(string path) =>
        path.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);

    private static string GetSelectedPath()
    {
        string selectedPath = "Assets";

        foreach (Object assetObject in Selection.GetFiltered(typeof(Object), SelectionMode.Assets))
        {
            selectedPath = AssetDatabase.GetAssetPath(assetObject);

            if (File.Exists(selectedPath))
                selectedPath = Path.GetDirectoryName(selectedPath);

            break;
        }

        return selectedPath;
    }

    private static string GetFeatureInstallerTemplate(string className) =>
$@"using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class {className} : IInstaller
{{
    public void Install(IContainerBuilder builder)
    {{
        throw new NotImplementedException();
    }}
}}
";

    private static string GetMediatorsInstallerTemplate(string className) =>
$@"using VContainer;
using VContainer.Unity;

public class {className} : IInstaller
{{
    public void Install(IContainerBuilder builder)
    {{
        throw new System.NotImplementedException();
    }}
}}
";
}
