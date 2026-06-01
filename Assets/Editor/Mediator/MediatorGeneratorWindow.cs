using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MediatorGeneratorWindow : EditorWindow
{
    public enum MediatorType
    {
        Standard,
        UIView,
        UIViews
    }

    private MediatorType _selectedType;
    private MonoScript _scriptA;
    private MonoScript _scriptB;
    private string _mediatorName = string.Empty;
    private bool _isNameManuallyEdited = false;

    [MenuItem("Assets/Create/Scripting/Mediator", false, priority = 3, secondaryPriority = 1f)]
    public static void CreateStandard() => Open(MediatorType.Standard);

    [MenuItem("Assets/Create/Scripting/UIView Mediator", false, priority = 3, secondaryPriority = 2f)]
    public static void CreateUIView() => Open(MediatorType.UIView);

    [MenuItem("Assets/Create/Scripting/UIViews Mediator", false, priority = 3, secondaryPriority = 3f)]
    public static void CreateUIViews() => Open(MediatorType.UIViews);

    public static void Open(MediatorType mediatorType)
    {
        MediatorGeneratorWindow window = GetWindow<MediatorGeneratorWindow>("Create Mediator");

        window._selectedType = mediatorType;
        window._scriptA = null;
        window._scriptB = null;
        window._isNameManuallyEdited = false;

        window.UpdateDefaultMediatorName();
        window.minSize = new Vector2(400, 240);
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label($"Create New {_selectedType} Mediator", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        EditorGUI.BeginChangeCheck();

        string labelA = _selectedType == MediatorType.Standard ? "Dependency A (Script)" : "Model/Logic (Script)";
        string labelB = _selectedType == MediatorType.Standard ? "Dependency B (Script)" : "View (Script)";

        _scriptA = EditorGUILayout.ObjectField(labelA, _scriptA, typeof(MonoScript), false) as MonoScript;
        EditorGUI.BeginChangeCheck();
        MonoScript selectedScriptB = EditorGUILayout.ObjectField(labelB, _scriptB, typeof(MonoScript), false) as MonoScript;

        if (EditorGUI.EndChangeCheck())
        {
            _scriptB = selectedScriptB;

            if (_selectedType == MediatorType.UIView || _selectedType == MediatorType.UIViews)
                ValidateViewScript();
        }

        if (EditorGUI.EndChangeCheck())
            if (!_isNameManuallyEdited)
                UpdateDefaultMediatorName();

        EditorGUI.BeginChangeCheck();
        _mediatorName = EditorGUILayout.TextField("Mediator Name", _mediatorName);

        if (EditorGUI.EndChangeCheck())
            _isNameManuallyEdited = true;

        if (_isNameManuallyEdited)
        {
            if (GUILayout.Button("Reset Name to Default Pattern"))
            {
                _isNameManuallyEdited = false;
                GUIUtility.keyboardControl = 0;
                UpdateDefaultMediatorName();
            }
        }

        EditorGUILayout.Space();

        bool isValid = _scriptA != null && _scriptB != null && !string.IsNullOrEmpty(_mediatorName);

        EditorGUI.BeginDisabledGroup(!isValid);

        if (GUILayout.Button("Generate File"))
            GenerateMediator();

        EditorGUI.EndDisabledGroup();
    }

    private void ValidateViewScript()
    {
        if (_scriptB == null)
            return;

        Type classType = _scriptB.GetClass();

        if (classType == null || !typeof(UIView).IsAssignableFrom(classType))
        {
            _scriptB = null;
            EditorUtility.DisplayDialog("Invalid View Type", "The selected script must inherit from UIView.", "OK");
        }
    }

    private void UpdateDefaultMediatorName()
    {
        string nameA = _scriptA != null ? _scriptA.name : "First";
        string nameB = _scriptB != null ? _scriptB.name : "Second";
        nameB = _selectedType == MediatorType.UIViews ? nameB + "s" : nameB;
        _mediatorName = $"{nameA}{nameB}Mediator";
    }

    private void GenerateMediator()
    {
        if (_scriptA == null)
            return;

        if (_scriptB == null)
            return;

        string directoryPath = GetSelectedPath();
        string filePath = Path.Combine(directoryPath, $"{_mediatorName}.cs");

        if (File.Exists(filePath))
            if (!EditorUtility.DisplayDialog("File Already Exists", $"Overwrite {filePath}?", "Yes", "No"))
                return;

        string code = _selectedType switch
        {
            MediatorType.Standard => GenerateStandardMediatorCode(_scriptA.name, _scriptB.name),
            MediatorType.UIView => GenerateUIViewMediatorCode(_scriptA.name, _scriptB.name),
            MediatorType.UIViews => GenerateUIViewsMediatorCode(_scriptA.name, _scriptB.name),
            _ => throw new ArgumentOutOfRangeException()
        };

        File.WriteAllText(filePath, code);
        AssetDatabase.Refresh();
        Close();
    }

    private string GetSelectedPath()
    {
        string path = "Assets";

        foreach (UnityEngine.Object assetObject in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
        {
            path = AssetDatabase.GetAssetPath(assetObject);

            if (File.Exists(path))
                path = Path.GetDirectoryName(path);

            break;
        }

        return path;
    }

    private string GenerateStandardMediatorCode(string dependencyA, string dependencyB)
    {
        string fieldA = GetPrivateFieldName(dependencyA);
        string fieldB = GetPrivateFieldName(dependencyB);

        string code = $@"using R3;

public class {_mediatorName} : {nameof(Mediator)}
{{
    private readonly {dependencyA} {fieldA};
    private readonly {dependencyB} {fieldB};

    public {_mediatorName}({dependencyA} {GetParamName(dependencyA)}, {dependencyB} {GetParamName(dependencyB)})
    {{
        {fieldA} = {GetParamName(dependencyA)};
        {fieldB} = {GetParamName(dependencyB)};
    }}

    protected override void Bind(CompositeDisposable disposables)
    {{
        throw new System.NotImplementedException();
    }}
}}
";
        return code;
    }

    private string GenerateUIViewMediatorCode(string dependencyA, string dependencyB)
    {
        string serviceField = GetPrivateFieldName(dependencyA);

        string code = $@"using R3;

public class {_mediatorName} : {nameof(UIViewMediator<UIView>)}<{dependencyB}>
{{
    private readonly {dependencyA} {serviceField};

    public {_mediatorName}({dependencyA} {GetParamName(dependencyA)}, {dependencyB} view)
        : base(view) => {serviceField} = {GetParamName(dependencyA)};

    protected override void OnViewEnabled({dependencyB} view, CompositeDisposable viewDisposables)
    {{
        throw new System.NotImplementedException();
    }}
}}
";
        return code;
    }

    private string GenerateUIViewsMediatorCode(string dependencyA, string dependencyB)
    {
        string serviceField = GetPrivateFieldName(dependencyA);

        string code = $@"using R3;
using System.Collections.Generic;

public class {_mediatorName} : {nameof(UIViewsMediator<UIView>)}<{dependencyB}>
{{
    private readonly {dependencyA} {serviceField};

    public {_mediatorName}({dependencyA} {GetParamName(dependencyA)}, IReadOnlyList<{dependencyB}> views)
        : base(views) => {serviceField} = {GetParamName(dependencyA)};

    protected override void OnViewEnabled({dependencyB} view, CompositeDisposable viewDisposables)
    {{
        throw new System.NotImplementedException();
    }}
}}
";
        return code;
    }

    private string GetPrivateFieldName(string typeName)
    {
        if (string.IsNullOrEmpty(typeName))
            return string.Empty;

        string fieldName = "_" + char.ToLowerInvariant(typeName[0]) + typeName[1..];
        return fieldName;
    }

    private string GetParamName(string typeName)
    {
        if (string.IsNullOrEmpty(typeName))
            return string.Empty;

        string paramName = char.ToLowerInvariant(typeName[0]) + typeName[1..];
        return paramName;
    }
}
