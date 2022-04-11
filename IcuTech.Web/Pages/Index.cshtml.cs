using System.ComponentModel;
using IcuTech.Web.Services.IcuTechApi;
using IcuTech.Web.Services.IcuTechApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IcuTech.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IIcuTechApiClient _icuTechApiClient;

    public IndexModel(
        ILogger<IndexModel> logger, 
        IIcuTechApiClient icuTechApiClient)
    {
        _logger = logger;
        _icuTechApiClient = icuTechApiClient;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public OutputModel Output { get; set; } = new();

    public class InputModel
    {
        [BindProperty]
        public string Login { get; set; }
        
        [BindProperty]
        public string Password { get; set; } 
    }
    
    public class OutputModel
    {
        public bool? IsSuccess { get; init; } = null;
        public string? Reason { get; init; } = "No data";
        public ILoginResponse Response { get; init; }
    }
    
    public void OnGet()
    {
    }

    public async Task OnPostAsync()
    {
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();

        _logger.LogWarning($"{ip}, {Input.Login}");
        
        var response = await _icuTechApiClient.LoginAsync(new LoginRequest
        {
            UserName = Input.Login,
            Password = Input.Password,
            IPs = ip
        });

        if (response != null && response.ResultCode != -1)
        {
            Output = new OutputModel
            {
                IsSuccess = true,
                Reason = response.Return,
                Response = response
            };
        }
        else if (response is { ResultMessage: { } })
        {
            Output = new OutputModel
            {
                IsSuccess = false,
                Reason = response.ResultMessage
            };
        }
        else
        {
            Output = new OutputModel
            {
                IsSuccess = false
            };
        }
    }
}