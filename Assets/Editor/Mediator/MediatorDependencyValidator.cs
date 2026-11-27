using Ovoshnig.Mediation;
using Ovoshnig.UI.Basic;
using System;
using UnityEditor;

namespace Ovoshnig.Editor.Mediation;

public static class MediatorDependencyValidator
{
    public static bool TryValidateScriptA(MonoScript scriptA, MediatorGeneratorWindow.MediatorType mediatorType,
        out string error)
    {
        if (!TryValidateCommon(scriptA, out error))
            return false;

        if (scriptA == null)
            return true;

        Type type = scriptA.GetClass();

        if (mediatorType != MediatorGeneratorWindow.MediatorType.Standard && typeof(UIView).IsAssignableFrom(type))
        {
            error = "Logic/Model dependency cannot inherit from UIView.";
            return false;
        }

        return true;
    }

    public static bool TryValidateScriptB(MonoScript scriptB, MediatorGeneratorWindow.MediatorType mediatorType,
        out string error)
    {
        if (!TryValidateCommon(scriptB, out error))
            return false;

        if (scriptB == null)
            return true;

        Type type = scriptB.GetClass();

        if (mediatorType != MediatorGeneratorWindow.MediatorType.Standard)
        {
            if (!typeof(UIView).IsAssignableFrom(type))
            {
                error = "View dependency must inherit from UIView.";
                return false;
            }
        }

        return true;
    }

    public static bool TryValidateDistinct(MonoScript scriptA, MonoScript scriptB, out string error)
    {
        error = string.Empty;

        if (scriptA == null || scriptB == null)
            return true;

        if (scriptA == scriptB)
        {
            error = "Dependencies cannot be the same script.";
            return false;
        }

        return true;
    }

    private static bool TryValidateCommon(MonoScript script, out string error)
    {
        error = string.Empty;

        if (script == null)
            return true;

        Type type = script.GetClass();

        if (type == null)
        {
            error = "Script has compilation errors or filename doesn't match class name.";
            return false;
        }

        if (IsStatic(type))
        {
            error = "Dependency cannot be a static class.";
            return false;
        }

        if (IsEditorType(type))
        {
            error = "Dependency cannot be an Editor-only script.";
            return false;
        }

        if (IsMediator(type))
        {
            error = "Dependency cannot be a Mediator.";
            return false;
        }

        return true;
    }

    private static bool IsStatic(Type type) => type.IsAbstract && type.IsSealed;

    private static bool IsEditorType(Type type) => type.Assembly.GetName().Name.Contains("Editor");

    private static bool IsMediator(Type type)
    {
        Type current = type;

        while (current != null && current != typeof(object))
        {
            if (current == typeof(Mediator))
                return true;

            if (current.IsGenericType)
            {
                Type genericDefinition = current.GetGenericTypeDefinition();

                if (genericDefinition == typeof(UIViewMediator<>) || genericDefinition == typeof(UIViewsMediator<>))
                    return true;
            }

            current = current.BaseType;
        }

        return false;
    }
}
