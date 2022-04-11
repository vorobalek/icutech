using IcuTech.Web.Soap;

namespace IcuTech.Web.Services.IcuTechApi;

public record IcuTechApiClientOptions : ISoapClientOptions
{
    public string? EndPoint { get; set; }
}