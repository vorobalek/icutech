using System.Xml.Linq;
using IcuTech.Web.Services.IcuTechApi.Dtos;
using IcuTech.Web.Soap;
using Microsoft.Extensions.Options;

namespace IcuTech.Web.Services.IcuTechApi;

public class IcuTechApiClient : 
    SoapClient<IcuTechApiClient>, 
    IIcuTechApiClient
{
    public IcuTechApiClient(
        IOptions<IcuTechApiClientOptions> options,
        ILogger<IcuTechApiClient> logger) 
        : base(logger)
    {
        Options = options.Value;
    }
    
    protected override ISoapClientOptions Options { get; }
    protected override XNamespace Xsi => "http://www.w3.org/2001/XMLSchema";
    protected override XNamespace Xsd => "http://www.w3.org/2001/XMLSchema-instance";
    
    public async Task<ILoginResponse?> LoginAsync(ILoginRequest request)
    {
        var responses = await ProcessSoapInternal<LoginRequest, LoginResponse>(request.Create());
        return responses.FirstOrDefault();
    }
}