using System.Xml.Linq;
using System.Xml.Serialization;
using IcuTech.Web.Soap;
using Newtonsoft.Json;

namespace IcuTech.Web.Services.IcuTechApi.Dtos;

public record LoginResponse : IcuTechApiResponse<LoginResponse>, ILoginResponse
{
    [JsonIgnore]
    [XmlElement("return")]
    public string? Return { get; set; }
    
    [XmlIgnore]
    public int? ResultCode { get; init; }
    
    [XmlIgnore]
    public string? ResultMessage { get; init; }
    
    [XmlIgnore]
    public int? EntityId { get; init; }
    
    [XmlIgnore]
    public string? FirstName { get; init; }
    
    [XmlIgnore]
    public string? LastName { get; init; }
    
    [XmlIgnore]
    public string? Company { get; init; }
    
    [XmlIgnore]
    public string? Address { get; init; }
    
    [XmlIgnore]
    public string? City { get; init; }
    
    [XmlIgnore]
    public string? Country { get; init; }
    
    [XmlIgnore]
    public string? Zip { get; init; }
    
    [XmlIgnore]
    public string? Phone { get; init; }
    
    [XmlIgnore]
    public string? Mobile { get; init; }
    
    [XmlIgnore]
    public string? Email { get; init; }
    
    [XmlIgnore]
    public ushort? EmailConfirm { get; init; }
    
    [XmlIgnore]
    public ushort? MobileConfirm { get; init; }
    
    [XmlIgnore]
    public int? CountryID { get; init; }
    
    [XmlIgnore]
    public int? Status { get; init; }
    
    [XmlIgnore]
    public string? lid { get; init; }
    
    [XmlIgnore]
    public string? FTPHost { get; init; }
    
    [XmlIgnore]
    public ushort? FTPPort { get; init; }

    public override LoginResponse FromXElement(XElement xml, XElement rawRequest, SoapResponseInternal rawResponse)
    {
        var xElement = new XElement(nameof(LoginResponse), xml);
        var response = base.FromXElement(xElement, rawRequest, rawResponse);
        var newResponse = JsonConvert.DeserializeObject<LoginResponse>(response.Return);
        newResponse.Return = response.Return;
        return newResponse;
    }
}