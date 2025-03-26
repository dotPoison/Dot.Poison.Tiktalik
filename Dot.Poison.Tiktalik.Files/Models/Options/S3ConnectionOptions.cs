using System.Diagnostics.CodeAnalysis;

namespace Dot.Poison.Tiktalik.Files.Models.Options;

public class S3ConnectionOptions
{
    [AllowNull] public string AWSAccessKeyId { get; set; }
    [AllowNull] public string AWSSecretAccessKey { get; set; }
    public bool UseHttps { get; set; }
    public string Host { get; set; } = "sds.tiktalik.com";
    public TimeSpan RequestTimeout { get; set; } = TimeSpan.FromMinutes(1);
}