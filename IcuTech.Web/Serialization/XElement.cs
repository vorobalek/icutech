using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace IcuTech.Web.Serialization;

public abstract record XElement<T>
    where T : XElement<T>, new()
{
    protected XElement ToXElement()
    {
        using var memoryStream = new MemoryStream();
        using TextWriter streamWriter = new StreamWriter(memoryStream);
        var xmlSerializer = new XmlSerializer(typeof(T));
        xmlSerializer.Serialize(streamWriter, this);
        var xElement = XElement.Parse(Encoding.UTF8.GetString(memoryStream.ToArray()));
        return xElement;
    }

    public static T FromXElement(XElement xElement)
    {
        var xmlSerializer = new XmlSerializer(typeof(T));
        return (xmlSerializer.Deserialize(xElement.CreateReader()) as T)!;
    }
}