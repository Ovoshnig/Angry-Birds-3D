using System.Linq;
using System.Xml.Linq;
using UnityEditor;

namespace ProjectEditor;

public class CSProjectPostprocessor : AssetPostprocessor
{
    private const string TargetFramework = "net10.0";
    private const string LangVersion = "14";

    private static string OnGeneratedCSProject(string path, string content)
    {
        XDocument document = XDocument.Parse(content, LoadOptions.PreserveWhitespace);

        XElement targetFramework = document.Descendants()
            .FirstOrDefault(e => e.Name.LocalName == "TargetFramework");

        if (targetFramework != null)
        {
            targetFramework.Value = TargetFramework;
        }
        else
        {
            XElement propertyGroup = document.Descendants()
                .FirstOrDefault(e => e.Name.LocalName == "AssemblyName")?.Parent;

            if (propertyGroup != null)
            {
                XNamespace @namespace = propertyGroup.Name.Namespace;
                propertyGroup.Add(new XElement(@namespace + "TargetFramework", TargetFramework));
            }
        }

        XElement langVersion = document.Descendants()
            .FirstOrDefault(e => e.Name.LocalName == "LangVersion");

        if (langVersion != null)
        {
            langVersion.Value = LangVersion;
        }
        else
        {
            XElement propertyGroup = (targetFramework ?? document.Descendants()
                .FirstOrDefault(e => e.Name.LocalName == "AssemblyName"))?.Parent;

            if (propertyGroup != null)
            {
                XNamespace @namespace = propertyGroup.Name.Namespace;
                propertyGroup.Add(new XElement(@namespace + "LangVersion", LangVersion));
            }
        }

        return document.ToString(SaveOptions.DisableFormatting);
    }
}
