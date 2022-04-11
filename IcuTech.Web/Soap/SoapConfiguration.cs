using System.Xml.Linq;

namespace IcuTech.Web.Soap;

public record SoapConfiguration
{
    public string MediaType { get; private set; }

    public XNamespace Schema { get; private set; }

    public XNamespace Encoding { get; private set; }

    public SoapVersion SoapVersion { get; private set; }

    public SoapConfiguration(SoapVersion soapVersion)
    {
        if (soapVersion == SoapVersion.Soap11)
        {
            MediaType = "text/xml";
            Schema = "http://schemas.xmlsoap.org/soap/envelope/";
            Encoding = "http://schemas.xmlsoap.org/soap/encoding/";
        }
        else
        {
            MediaType = "application/soap+xml";
            Schema = "http://www.w3.org/2003/05/soap-envelope";
        }

        SoapVersion = soapVersion;
    }
}