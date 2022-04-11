namespace IcuTech.Web.Services.IcuTechApi.Dtos;

public record LoginRequest : IcuTechApiRequest<LoginRequest>, ILoginRequest
{
    public override string RequestType => "Login";

    public string UserName { get; set; }
    public string Password { get; set; }
    public string? IPs { get; set; }
    
    public override LoginRequest Create()
    {
        return this;
    }
}