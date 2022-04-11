using System.Xml.Linq;
using Flurl.Http;

namespace IcuTech.Web.Soap;

public abstract class SoapClient<TClient>
    where TClient : SoapClient<TClient>
{
    private readonly ILogger<TClient> _logger;
    
    // ReSharper disable once ContextualLoggerProblem
    protected SoapClient(ILogger<TClient> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    protected abstract ISoapClientOptions Options { get; }
    protected abstract XNamespace Xsi { get; }
    protected abstract XNamespace Xsd { get; }
    
    protected async Task<TResponse[]> ProcessSoapInternal<TRequest, TResponse>(TRequest request)
        where TRequest : SoapRequest<TRequest>, new()
        where TResponse : SoapResponse<TResponse>, new()
    {
        var body = new XElement(
            request.Namespace + request.RequestType,
            new XAttribute(XNamespace.Xmlns + "xsi", Xsi),
            new XAttribute(XNamespace.Xmlns + "xsd", Xsd)
        );
        foreach (var xElement in request.ToBody())
        {
            body.Add(xElement);
        }
        SoapResponseInternal response;

        try
        {
            response = await this.PostSoap(body, request.Url);
        }
        catch (Exception ex)
        {
            if (ex.InnerException is not FlurlHttpException flurlHttpException) 
                return new[] { SoapResponse<TResponse>.Fail(body, ex.Message) };
                
            var message = await flurlHttpException.Call.Response.ResponseMessage.Content.ReadAsStringAsync();
            return new[] { SoapResponse<TResponse>.Fail(body, string.Join(Environment.NewLine, ex.Message, message)) };
        }

        var returns = response
            .Body
            ?.Element(request.Namespace + request.ResponseType)?
            .Elements("return")
            .Select(xml => new TResponse().FromXElement(xml, body, response))
            .ToArray();

        return returns ?? Array.Empty<TResponse>();
    }
    
    private async Task<SoapResponseInternal> PostSoap(XElement body, string url)
    {
        if (string.IsNullOrWhiteSpace(Options.EndPoint))
            throw new InvalidOperationException(
                $"{nameof(Options.EndPoint)} can not be null or empty or whitespace string");
        
        string? request = null;
        try
        {
            var response = await $"{Options.EndPoint}/{url}"
                .ConfigureRequest(settings => settings.BeforeCall = call => request = call.RequestBody)
                .PostSoapAsync(SoapVersion.Soap11, body);

            var result = await response.ResponseMessage.ParseSoapResponse(SoapVersion.Soap11);

            _logger.LogTrace("Request: {Request}, Response: {Response}", request ?? body.ToString(), result.ToString());
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError("Request: {Request}, Response: {Response}", request ?? body.ToString(), ex.Message);
            throw new Exception("An error while processing soap request", ex);
        }
    }
}