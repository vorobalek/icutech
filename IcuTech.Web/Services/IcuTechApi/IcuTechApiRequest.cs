using System.Xml.Linq;
using IcuTech.Web.Soap;

namespace IcuTech.Web.Services.IcuTechApi;

public abstract record IcuTechApiRequest<T> : SoapRequest<T> 
    where T : IcuTechApiRequest<T>, new()
{
    public override string Url => "icutech-test.dll/soap/IICUTech";
    public override XNamespace Namespace => "urn:ICUTech.Intf-IICUTech";
    public override string ResponseType => $"{RequestType}Response";

    public override IEnumerable<XElement> ToBody()
    {
        var xElement = ToXElement();
        return xElement.Elements();
    }
}