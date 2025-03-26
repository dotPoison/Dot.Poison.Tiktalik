using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Dot.Poison.Tiktalik.Files.Extensions;
using Dot.Poison.Tiktalik.Files.Interfaces;
using Dot.Poison.Tiktalik.Files.Models.Http.Response;

namespace Dot.Poison.Tiktalik.Files.Tests;

[TestFixture]
public class ListBucketsTests
{
    [Test]
    public async Task GetAllBucketsAsync()
    {
        IConfiguration configuration = new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly()).Build();
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton(configuration);
        serviceCollection.AddTiktalikS3FileServices((options, provider) =>
        {
            IConfiguration _configuration = provider.GetRequiredService<IConfiguration>();
            options.AWSAccessKeyId = _configuration["Tiktalik:Files:AWSAccessKeyId"];
            options.AWSSecretAccessKey = _configuration["Tiktalik:Files:AWSSecretAccessKey"];
            options.RequestTimeout = TimeSpan.FromSeconds(5);
        });

        ServiceProvider provider = serviceCollection.BuildServiceProvider();
        IS3Files s3Files = provider.GetRequiredService<IS3Files>();
        ListBucketsResponse listBucketsResponse = await s3Files.GetAllBucketsAsync();
        Assert.That(listBucketsResponse, Is.Not.Null);
    }
}