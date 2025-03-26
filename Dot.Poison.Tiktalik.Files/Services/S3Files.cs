using Dot.Poison.Tiktalik.Files.Extensions;
using Dot.Poison.Tiktalik.Files.Interfaces;
using Dot.Poison.Tiktalik.Files.Serialization;
using Dot.Poison.Tiktalik.Files.Models.Http.Response;

namespace Dot.Poison.Tiktalik.Files.Services;

public class S3Files(S3Connection connection) : IS3Files
{
    public ListBucketsResponse GetAllBuckets() => GetAllBucketsAsync().RunTaskSync();

    public async Task<ListBucketsResponse> GetAllBucketsAsync(CancellationToken cancellationToken = default)
    {
        using HttpClient client = connection.GetHttpClient();
        using HttpResponseMessage httpResponse = await client.GetAsync("/", cancellationToken);
        await using Stream contentStream = await httpResponse.Content.ReadAsStreamAsync(cancellationToken);
        ListBucketsResponse response = S3ResponseSerializer.Instance.Deserialize<ListBucketsResponse>(contentStream);
        return response;
    }
}
