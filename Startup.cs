using Microsoft.Extensions.Configuration;

public class Startup
{
    private readonly IConfiguration _config;

    public Startup(IConfiguration config)
    {
        _config = config;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Add configuration to the DI container
        services.AddSingleton(_config);

        // ...
    }

    // ...
}
