using Dot.Poison.Tiktalik.Files.Models.Http.Response;

namespace Dot.Poison.Tiktalik.Files.Interfaces;

public interface IS3Files
{
    public ListBucketsResponse GetAllBuckets();
    public Task<ListBucketsResponse> GetAllBucketsAsync(CancellationToken cancellationToken = default);
}