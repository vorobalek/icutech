using IcuTech.Web.Soap;

namespace IcuTech.Web.Services.IcuTechApi.Dtos;

public interface ILoginRequest : ISoapRequest<LoginRequest>
{
    string UserName { get; set; }
    string Password { get; set; }
    string IPs { get; set; }
}