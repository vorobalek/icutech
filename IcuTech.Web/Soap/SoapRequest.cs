using System.Xml.Linq;
using System.Xml.Serialization;
using IcuTech.Web.Serialization;

namespace IcuTech.Web.Soap;

public abstract record SoapRequest<T> : XElement<T>, ISoapRequest<T>
    where T : SoapRequest<T>, new()
{
    [XmlIgnore]
    public abstract string Url { get; }
    
    [XmlIgnore]
    public abstract XNamespace Namespace { get; }
    
    [XmlIgnore]
    public abstract string RequestType { get; }
    
    [XmlIgnore]
    public abstract string ResponseType { get; }

    public abstract T Create();

    public virtual IEnumerable<XElement> ToBody()
    {
        var xElement = ToXElement();
        return new[] { xElement };
    }
}