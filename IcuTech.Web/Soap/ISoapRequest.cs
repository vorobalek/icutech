using System.Xml.Linq;

namespace IcuTech.Web.Soap;

public interface ISoapRequest<out T>
    where T : SoapRequest<T>, new()
{
    string Url { get; }
    XNamespace Namespace { get; }
    string RequestType { get; }
    string ResponseType { get; }
    T Create();
}