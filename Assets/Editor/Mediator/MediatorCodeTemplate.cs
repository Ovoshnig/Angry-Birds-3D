using Ovoshnig.Mediation;
using Ovoshnig.UI.Basic;
using System;

namespace Ovoshnig.Editor.Mediation;

public static class MediatorCodeTemplate
{
    public static string Create(MediatorGeneratorWindow.MediatorType type, string mediatorName, string dependencyA,
        string dependencyB)
    {
        return type switch
        {
            MediatorGeneratorWindow.MediatorType.Standard => GenerateStandard(mediatorName, dependencyA, dependencyB),
            MediatorGeneratorWindow.MediatorType.UIView => GenerateUIView(mediatorName, dependencyA, dependencyB),
            MediatorGeneratorWindow.MediatorType.UIViews => GenerateUIViews(mediatorName, dependencyA, dependencyB),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    private static string GenerateStandard(string mediatorName, string dependencyA, string dependencyB)
    {
        string fieldA = GetPrivateFieldName(dependencyA);
        string fieldB = GetPrivateFieldName(dependencyB);

        return
            $$"""
            using {{typeof(Mediator).Namespace}};
            using R3;

            namespace ;

            public class {{mediatorName}} : {{nameof(Mediator)}}
            {
                private readonly {{dependencyA}} {{fieldA}};
                private readonly {{dependencyB}} {{fieldB}};

                public {{mediatorName}}({{dependencyA}} {{GetParamName(dependencyA)}}, {{dependencyB}} {{GetParamName(dependencyB)}})
                {
                    {{fieldA}} = {{GetParamName(dependencyA)}};
                    {{fieldB}} = {{GetParamName(dependencyB)}};
                }

                protected override void Bind(CompositeDisposable disposables)
                {
                    throw new System.NotImplementedException();
                }
            }

            """;
    }

    private static string GenerateUIView(string mediatorName, string dependencyA, string dependencyB)
    {
        string serviceField = GetPrivateFieldName(dependencyA);

        return
            $$"""
            using {{typeof(UIViewMediator<>).Namespace}};
            using R3;

            namespace ;

            public class {{mediatorName}} : {{nameof(UIViewMediator<>)}}<{{dependencyB}}>
            {
                private readonly {{dependencyA}} {{serviceField}};

                public {{mediatorName}}({{dependencyA}} {{GetParamName(dependencyA)}}, {{dependencyB}} view)
                    : base(view) => {{serviceField}} = {{GetParamName(dependencyA)}};

                protected override void OnViewEnabled({{dependencyB}} view, CompositeDisposable viewDisposables)
                {
                    throw new System.NotImplementedException();
                }
            }

            """;
    }

    private static string GenerateUIViews(string mediatorName, string dependencyA, string dependencyB)
    {
        string serviceField = GetPrivateFieldName(dependencyA);

        return
            $$"""
            using {{typeof(UIViewsMediator<>).Namespace}};
            using R3;
            using System.Collections.Generic;

            namespace ;

            public class {{mediatorName}} : {{nameof(UIViewsMediator<>)}}<{{dependencyB}}>
            {
                private readonly {{dependencyA}} {{serviceField}};

                public {{mediatorName}}({{dependencyA}} {{GetParamName(dependencyA)}}, IReadOnlyList<{{dependencyB}}> views)
                    : base(views) => {{serviceField}} = {{GetParamName(dependencyA)}};

                protected override void OnViewEnabled({{dependencyB}} view, CompositeDisposable viewDisposables)
                {
                    throw new System.NotImplementedException();
                }
            }

            """;
    }

    private static string GetPrivateFieldName(string typeName) => ToCamelCase(typeName, "_");

    private static string GetParamName(string typeName) => ToCamelCase(typeName);

    private static string ToCamelCase(string typeName, string prefix = "") =>
        string.IsNullOrEmpty(typeName)
            ? string.Empty
            : prefix + char.ToLowerInvariant(typeName[0]) + typeName[1..];
}
