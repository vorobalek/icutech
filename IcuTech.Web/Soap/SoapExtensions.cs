using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;
using Flurl.Http;
using Flurl.Http.Content;

namespace IcuTech.Web.Soap;

public static class SoapExtensions
{
    public static Task<IFlurlResponse> PostSoapAsync(
        this IFlurlRequest endpoint,
        SoapVersion soapVersion,
        XElement body,
        XElement? header = null,
        string? action = null, 
        string? actionName = null)
        => endpoint.PostSoapAsync(
            soapVersion,
            new[] { body },
            header != null ? new[] { header } : default(IEnumerable<XElement>),
            action, actionName);
    
    public static async Task<SoapResponseInternal> ParseSoapResponse(this HttpResponseMessage message, SoapVersion soapVersion)
    {
        var responseText = Encoding.UTF8.GetString(await message.Content.ReadAsByteArrayAsync());
        if(!message.IsSuccessStatusCode)
            throw new SoapException($"StatusCode: {message.StatusCode}, Response: {responseText}");

        switch (soapVersion)
        {
            case SoapVersion.Soap11:
                return new SoapResponseV11(responseText);
            case SoapVersion.Soap12:
                return new SoapResponseV11(responseText);
            default:
                throw new ArgumentOutOfRangeException(nameof(soapVersion), soapVersion, null);
        }
    }
    
    private static XElement GetEnvelope(this SoapConfiguration soapMessageConfiguration)
    {
        var envelope = new XElement(
            soapMessageConfiguration.Schema + "Envelope",
            new XAttribute(XNamespace.Xmlns + "SOAP-ENV", soapMessageConfiguration.Schema.NamespaceName));
        
        if (soapMessageConfiguration.SoapVersion == SoapVersion.Soap11)
        {
            envelope.Add(new XAttribute(soapMessageConfiguration.Schema+ "encodingStyle", soapMessageConfiguration.Encoding));
        }
        return envelope;
    }

    private static Task<IFlurlResponse> PostSoapAsync(
        this IFlurlRequest endpoint, 
        SoapVersion soapVersion,
        IEnumerable<XElement> bodies,
        IEnumerable<XElement>? headers = null,
        string? action = null, 
        string? actionName = null)
    {
        if (endpoint == null)
            throw new ArgumentNullException(nameof(endpoint));

        if (bodies == null)
            throw new ArgumentNullException(nameof(bodies));

        if (!bodies.Any())
            throw new ArgumentException("Bodies element cannot be empty", nameof(bodies));

        // Get configuration based on version
        var messageConfiguration = new SoapConfiguration(soapVersion);

        // Get the envelope
        var envelope = messageConfiguration.GetEnvelope();

        // Add headers
        if (headers != null && headers.Any())
            envelope.Add(new XElement(messageConfiguration.Schema + "Header", headers));

        // Add bodies
        envelope.Add(new XElement(messageConfiguration.Schema + "Body", bodies));
        
        // Get HTTP content
        var content = new CapturedStringContent("<?xml version='1.0' encoding='utf-8'?>" + Environment.NewLine + envelope, messageConfiguration.MediaType);
        
        // Add SOAP action if any
        if (action != null && messageConfiguration.SoapVersion == SoapVersion.Soap11)
        {
            var headerName = "ActionHeader";

            if (actionName != null)
            {
                headerName = actionName;
            }

            content.Headers.Add(headerName, action);
        }
            
        else if (action != null && messageConfiguration.SoapVersion == SoapVersion.Soap12)
            content.Headers.ContentType?.Parameters.Add(new NameValueHeaderValue("ActionParameter", $"\"{action}\""));

        return endpoint.PostAsync(content);
    }
}