using System.Xml.Linq;

namespace IcuTech.Web.Soap;

public abstract record SoapResponseInternal
{
    protected readonly string _xmlResponse;
    protected readonly SoapConfiguration _configuration;
    protected XDocument _doc;

    protected SoapResponseInternal(SoapVersion soapVersion, string message)
    {
        _configuration = new SoapConfiguration(soapVersion);
        _xmlResponse = message;
        _doc = XDocument.Parse(this._xmlResponse);
    }

    public bool IsSuccess { get; protected set; } = true;

    public XDocument Doc => this._doc;

    public string XmlResponse => this._xmlResponse;

    public XElement? Body => this.Envelope.Element(this._configuration.Schema + nameof (Body));
    public XElement Envelope => this.Doc.Descendants().First();

    public abstract XElement? GetResult(string? tagName);
    public abstract XElement? GetResult(XName tagName);
    public abstract XElement? GetBodyResult(string? tagName = null);

    public override string ToString() => this._xmlResponse;
}