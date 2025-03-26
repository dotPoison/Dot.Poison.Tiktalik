using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;

using Dot.Poison.Tiktalik.Files.Services;
using Dot.Poison.Tiktalik.Files.Interfaces;
using Dot.Poison.Tiktalik.Files.Models.Options;

namespace Dot.Poison.Tiktalik.Files.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddTiktalikS3FileServices(this IServiceCollection services, Action<S3ConnectionOptions, IServiceProvider> configure)
    {
        services.AddHttpClient("S3HttpClient", (provider, httpclient) =>
        {
            var options = provider.GetRequiredService<IOptions<S3ConnectionOptions>>().Value;
            UriBuilder builder = new()
            {
                Host = options.Host,
                Scheme = options.UseHttps ? "https" : "http",
                Port = -1 // -1 used for not localhost
            };
            httpclient.BaseAddress = builder.Uri;
            httpclient.Timeout = options.RequestTimeout;
        });
        
        services.AddScoped<IS3Files, S3Files>();
        services.AddScoped<S3Connection>(provider => new S3Connection(
            provider.GetRequiredService<IOptions<S3ConnectionOptions>>(),
            provider.GetRequiredService<IHttpClientFactory>()));
        
        services.AddOptions<S3ConnectionOptions>().Configure(configure);
    }
}