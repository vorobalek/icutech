using IcuTech.Web.Soap;

namespace IcuTech.Web.Services.IcuTechApi;

public abstract record IcuTechApiResponse<T> : SoapResponse<T>
    where T : IcuTechApiResponse<T>, new();