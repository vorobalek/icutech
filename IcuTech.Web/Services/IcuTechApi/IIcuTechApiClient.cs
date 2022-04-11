using IcuTech.Web.Services.IcuTechApi.Dtos;

namespace IcuTech.Web.Services.IcuTechApi;

public interface IIcuTechApiClient
{
    Task<ILoginResponse?> LoginAsync(ILoginRequest request);
}