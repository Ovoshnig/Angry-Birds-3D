using System.IO;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Ovoshnig.Editor.Mediation;

public class MediatorGeneratorWindow : EditorWindow
{
    public enum MediatorType
    {
        Standard,
        UIView,
        UIViews
    }

    [SerializeField] private MediatorType _selectedType;
    [SerializeField] private MonoScript _scriptA;
    [SerializeField] private MonoScript _scriptB;
    [SerializeField] private string _mediatorName = string.Empty;

    private SerializedObject _serializedObject;
    private Label _titleLabel;
    private ObjectField _scriptAField;
    private HelpBox _helpBoxA;
    private ObjectField _scriptBField;
    private HelpBox _helpBoxB;
    private TextField _nameField;
    private Button _resetNameButton;
    private Button _generateButton;

    public static void Open(MediatorType mediatorType)
    {
        MediatorGeneratorWindow window = GetWindow<MediatorGeneratorWindow>("Create Mediator");
        window.minSize = new Vector2(400, 280);
        window.Initialize(mediatorType);
        window.Show();
    }

    [MenuItem("Assets/Create/Scripting/Mediator", false, priority = 3, secondaryPriority = 1f)]
    private static void CreateStandard() => Open(MediatorType.Standard);

    [MenuItem("Assets/Create/Scripting/UIView Mediator", false, priority = 3, secondaryPriority = 2f)]
    private static void CreateUIView() => Open(MediatorType.UIView);

    [MenuItem("Assets/Create/Scripting/UIViews Mediator", false, priority = 3, secondaryPriority = 3f)]
    private static void CreateUIViews() => Open(MediatorType.UIViews);

    private static void UpdateHelpBox(HelpBox helpBox, bool isValid, string message)
    {
        helpBox.text = message;
        helpBox.style.display = isValid || string.IsNullOrEmpty(message) ? DisplayStyle.None : DisplayStyle.Flex;
    }

    private static string GetSelectedPath()
    {
        string path = "Assets";

        foreach (Object assetObject in Selection.GetFiltered<Object>(SelectionMode.Assets))
        {
            path = AssetDatabase.GetAssetPath(assetObject);

            if (File.Exists(path))
                path = Path.GetDirectoryName(path);

            break;
        }

        return path;
    }

    private void Initialize(MediatorType mediatorType)
    {
        _selectedType = mediatorType;
        _scriptA = null;
        _scriptB = null;
        _mediatorName = GetDefaultMediatorName();

        _serializedObject?.Update();
        Undo.ClearUndo(this);

        RefreshViewLabels();
        ResetNameToDefaultPattern();
    }

    private void CreateGUI()
    {
        _serializedObject = new SerializedObject(this);

        VisualElement root = rootVisualElement;
        IStyle style = root.style;
        style.paddingTop = 8;
        style.paddingBottom = 8;
        style.paddingLeft = 8;
        style.paddingRight = 8;

        _titleLabel = new Label()
        {
            style = { unityFontStyleAndWeight = FontStyle.Bold, marginBottom = 10 }
        };
        root.Add(_titleLabel);

        _scriptAField = new ObjectField()
        {
            bindingPath = nameof(_scriptA),
            objectType = typeof(MonoScript),
            allowSceneObjects = false
        };
        _scriptAField.RegisterValueChangedCallback(OnScriptChanged);
        root.Add(_scriptAField);

        _helpBoxA = new HelpBox(string.Empty, HelpBoxMessageType.Error);
        _helpBoxA.style.display = DisplayStyle.None;
        root.Add(_helpBoxA);

        _scriptBField = new ObjectField()
        {
            bindingPath = nameof(_scriptB),
            objectType = typeof(MonoScript),
            allowSceneObjects = false,
            style = { marginTop = 4 }
        };
        _scriptBField.RegisterValueChangedCallback(OnScriptChanged);
        root.Add(_scriptBField);

        _helpBoxB = new HelpBox(string.Empty, HelpBoxMessageType.Error);
        _helpBoxB.style.display = DisplayStyle.None;
        root.Add(_helpBoxB);

        _nameField = new TextField("Mediator Name")
        {
            bindingPath = nameof(_mediatorName),
            style = { marginTop = 6 }
        };
        _nameField.RegisterValueChangedCallback(_ => UpdateControlsState());
        root.Add(_nameField);

        _resetNameButton = new Button(ResetNameToDefaultPattern)
        {
            text = "Reset Name to Default Pattern",
            style = { marginTop = 4 }
        };
        root.Add(_resetNameButton);

        VisualElement spacer = new();
        spacer.style.flexGrow = 1;
        root.Add(spacer);

        _generateButton = new Button(GenerateMediator)
        {
            text = "Generate File",
            style = { height = 28 }
        };
        root.Add(_generateButton);

        root.Bind(_serializedObject);

        RefreshViewLabels();
        UpdateControlsState();
    }

    private void RefreshViewLabels()
    {
        _titleLabel.text = $"Create New {_selectedType} Mediator";

        if (_selectedType == MediatorType.Standard)
        {
            _scriptAField.label = "Dependency A (Script)";
            _scriptBField.label = "Dependency B (Script)";
            return;
        }

        _scriptAField.label = "Model/Logic (Script)";
        _scriptBField.label = "View (Script)";
    }

    private void OnScriptChanged(ChangeEvent<Object> _)
    {
        _serializedObject.Update();
        _serializedObject.FindProperty(nameof(_mediatorName)).stringValue = GetDefaultMediatorName();
        _serializedObject.ApplyModifiedPropertiesWithoutUndo();

        UpdateControlsState();
    }

    private void ResetNameToDefaultPattern()
    {
        _serializedObject.Update();
        _serializedObject.FindProperty(nameof(_mediatorName)).stringValue = GetDefaultMediatorName();
        _serializedObject.ApplyModifiedProperties();

        UpdateControlsState();
    }

    private string GetDefaultMediatorName()
    {
        string nameA = _scriptA != null ? _scriptA.name : "First";
        string nameB = _scriptB != null ? _scriptB.name : "Second";

        if (_selectedType == MediatorType.UIViews)
            nameB += "s";

        return $"{nameA}{nameB}Mediator";
    }

    private void UpdateControlsState()
    {
        bool isScriptAValid = MediatorDependencyValidator.TryValidateScriptA(_scriptA, _selectedType,
            out string errorA);
        bool isScriptBValid = MediatorDependencyValidator.TryValidateScriptB(_scriptB, _selectedType,
            out string errorB);
        bool areDistinct = MediatorDependencyValidator.TryValidateDistinct(_scriptA, _scriptB,
            out string errorDistinct);

        if (isScriptBValid && !areDistinct)
        {
            isScriptBValid = false;
            errorB = errorDistinct;
        }

        UpdateHelpBox(_helpBoxA, isScriptAValid, errorA);
        UpdateHelpBox(_helpBoxB, isScriptBValid, errorB);

        bool canGenerate = _scriptA != null && _scriptB != null && isScriptAValid && isScriptBValid && areDistinct
            && !string.IsNullOrWhiteSpace(_mediatorName);
        _generateButton.SetEnabled(canGenerate);
        _resetNameButton.style.display = _mediatorName != GetDefaultMediatorName()
            ? DisplayStyle.Flex
            : DisplayStyle.None;
    }

    private void GenerateMediator()
    {
        if (_scriptA == null || _scriptB == null)
            return;

        string directoryPath = GetSelectedPath();
        string filePath = Path.Combine(directoryPath, $"{_mediatorName}.cs");

        if (File.Exists(filePath)
            && !EditorUtility.DisplayDialog("File Already Exists", $"Overwrite {filePath}?", "Yes", "No"))
        {
            return;
        }

        string code = MediatorCodeTemplate.Create(_selectedType, _mediatorName, _scriptA.name, _scriptB.name);
        File.WriteAllText(filePath, code);
        AssetDatabase.Refresh();
        Close();

        Object createdAsset = AssetDatabase.LoadAssetAtPath<Object>(filePath);

        if (createdAsset == null)
            return;

        Selection.activeObject = createdAsset;
    }
}
