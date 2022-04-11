using System.Xml.Linq;
using System.Xml.Serialization;
using IcuTech.Web.Serialization;
using Newtonsoft.Json;

namespace IcuTech.Web.Soap;

public record SoapResponse<T> : XElement<T>, ISoapResponse
    where T : SoapResponse<T>, new()
{
    [JsonIgnore]
    [XmlIgnore]
    public bool SuccessInternal { get; set; }
    
    [JsonIgnore]
    [XmlIgnore]
    public string? ErrorMessageInternal { get; set; }
    
    [JsonIgnore]
    [XmlIgnore]
    public string? RequestInternal { get; set; }

    [JsonIgnore]
    [XmlIgnore]
    public string? ResponseInternal { get; set; }
    
    public virtual T FromXElement(XElement xml, XElement rawRequest, SoapResponseInternal rawResponse)
    {
        try
        {
            var dto = FromXElement(xml);

            dto.SuccessInternal = true;
            dto.RequestInternal = rawRequest.ToXml();
            dto.ResponseInternal= rawResponse.Body?.Value;
            return dto;
        }
        catch (Exception ex)
        {
            var fail = Fail(rawRequest, ex.Message);
            fail.RequestInternal = rawRequest.ToXml();
            fail.ResponseInternal = rawResponse.Body?.Value;
            return fail;
        }
    }

    public static T Fail(XElement rawRequest, string internalErrorMessage)
    {
        return new T
        {
            SuccessInternal = false,
            RequestInternal = rawRequest.ToXml(),
            ResponseInternal = null,
            ErrorMessageInternal = internalErrorMessage
        };
    }
}