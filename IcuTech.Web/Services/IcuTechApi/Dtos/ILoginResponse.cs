using System.Text.Json.Serialization;
using IcuTech.Web.Soap;

namespace IcuTech.Web.Services.IcuTechApi.Dtos;

public interface ILoginResponse : ISoapResponse
{
    [JsonIgnore]
    string? Return { get; }
    
    int? ResultCode { get; }
    string? ResultMessage { get; }
    int? EntityId { get; }
    string? FirstName { get; }
    string? LastName { get; }
    string? Company { get; }
    string? Address { get; }
    string? City { get; }
    string? Country { get; }
    string? Zip { get; }
    string? Phone { get; }
    string? Mobile { get; }
    string? Email { get; }
    ushort? EmailConfirm { get; }
    ushort? MobileConfirm { get; }
    int? CountryID { get; }
    int? Status { get; }
    string? lid { get; }
    string? FTPHost { get; }
    ushort? FTPPort { get; }
}