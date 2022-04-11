using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace IcuTech.Web.Serialization;

public static class XmlExtensions
{
    public static string? ToXml(
        this object? data,
        XmlWriterSettings? settings = null,
        XmlSerializerNamespaces? namespaces = null)
    {
        if (data == null)
            return null;
        var xmlSerializer = new XmlSerializer(data.GetType());
        settings ??= new XmlWriterSettings
        {
            Encoding = Encoding.UTF8
        };
        
        using var output = new EncodingStringWriter(settings.Encoding);
        using var xmlWriter = XmlWriter.Create(output, settings);
        xmlSerializer.Serialize(xmlWriter, data, namespaces);
        return output.ToString();
    }
}