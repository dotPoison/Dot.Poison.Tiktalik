namespace Dot.Poison.Tiktalik.Files.Models.Http.Common;

[XmlRoot("Buckets")]
public sealed class Buckets
{
    [XmlElement("Bucket")]
    public Bucket[] BucketList { get; set; } = [];
}
