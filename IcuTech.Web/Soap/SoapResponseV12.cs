using System.Xml.Linq;

namespace IcuTech.Web.Soap;

internal record SoapResponseV12 : SoapResponseInternal
{
    public SoapResponseV12(string message) : base(SoapVersion.Soap12, message)
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
                ? node 
                : node?.Element(tagName);
    }
}