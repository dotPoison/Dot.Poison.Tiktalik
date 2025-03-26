using System.Text;
using System.Globalization;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;

using Dot.Poison.Tiktalik.Files.Models.Options;

namespace Dot.Poison.Tiktalik.Files.Services;

public class S3Connection
{
    private readonly S3ConnectionOptions _configuration;
    private readonly IHttpClientFactory? _httpClientFactory;

    
    public S3Connection(IOptions<S3ConnectionOptions> configuration)
    {
        _configuration = configuration.Value;
    }

    public S3Connection(IOptions<S3ConnectionOptions> configuration, IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration.Value;
        _httpClientFactory = httpClientFactory;
    }

    public HttpClient GetHttpClient()
    {
        HttpClient client = _httpClientFactory?.CreateClient("S3HttpClient") ?? CreateHttpClient();
        client.DefaultRequestHeaders.Add("Host", _configuration.Host);
        client.DefaultRequestHeaders.Add("Date", FormatToGMT(DateTime.UtcNow));
        client.DefaultRequestHeaders.Add("Authorization", CreateAwsV2Signature());
        return client;
    }

    public string CreateAwsV2Signature()
    {
        string date = FormatToGMT(DateTime.UtcNow);
        string stringToSign = $"GET\n\n\n{date}\n/";
        string signature = SignString(stringToSign, _configuration.AWSSecretAccessKey);
        return $"AWS {_configuration.AWSAccessKeyId}:{signature}";
    }
    
    public static string SignString(string stringToSign, string secretKey)
    {
        using HMACSHA1 hmac = new(Encoding.UTF8.GetBytes(secretKey));
        byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign));
        return Convert.ToBase64String(hash);
    }
    
    private static string FormatToGMT(DateTime dateTime) => dateTime.ToString("ddd, dd MMM yyyy HH:mm:ss \\G\\M\\T", CultureInfo.InvariantCulture);

    private HttpClient CreateHttpClient()
    {
        UriBuilder builder = new()
        {
            Host = _configuration.Host,
            Scheme = _configuration.UseHttps ? "https" : "http",
            Port = -1 // -1 used for not localhost
        };
        HttpClient httpClient = new()
        {
            BaseAddress = builder.Uri,
            Timeout = _configuration.RequestTimeout
        };
        return httpClient;
    }
}
