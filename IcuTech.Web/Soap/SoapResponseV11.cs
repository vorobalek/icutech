using System.Xml.Linq;

namespace IcuTech.Web.Soap;

internal record SoapResponseV11 : SoapResponseInternal
{
    public SoapResponseV11(string message) : base(SoapVersion.Soap11, message)
    {

    }

    public override XElement? GetResult(string? tagName)
    {
        var node = Envelope.Descendants().First().Descendants().First();
        return 
            tagName == null 
                ? node.Descendants().First() 
                : node.Element(tagName);
    }

    public override XElement? GetResult(XName? tagName)
    {
        var node = Envelope.Descendants().First().Descendants().First();
        return 
            tagName == null
                ? node.Descendants().First() 
                : node.Element(tagName);
    }

    public override XElement? GetBodyResult(string? tagName = null)
    {
        var node = Body?.Descendants().First();

        return 
            tagName == null 
                ? node?.Descendants().First() 
                : node?.Element(tagName);
    }
}