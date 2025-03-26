namespace Dot.Poison.Tiktalik.Files.Models.Http.Response;

[XmlRoot("ListAllMyBucketsResult")]
public class ListBucketsResponse
{
    [XmlElement("Owner")] public Common.BucketOwner Owner { get; set; }
    [XmlElement("Buckets")] public Common.Buckets Buckets { get; set; }
}
