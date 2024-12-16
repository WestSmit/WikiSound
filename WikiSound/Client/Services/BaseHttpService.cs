using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace WikiSound.Client.Services
{
    internal abstract class BaseHttpService
    {
        protected Uri _apiUrl { get; }

        protected BaseHttpService(IWebAssemblyHostEnvironment hostEnvironmen, IConfiguration config) 
        {
            var env = hostEnvironmen.Environment;

            var apiUrlConfigValue = config.GetRequiredSection(env).GetValue<Uri>("ApiUrl");

            if (apiUrlConfigValue is null)
            {
                throw new Exception("API Url is not set");
            };

            _apiUrl = apiUrlConfigValue;
        }
    }
}
