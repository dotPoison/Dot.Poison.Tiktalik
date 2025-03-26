namespace Dot.Poison.Tiktalik.Files.Models.Http.Common;

[XmlRoot("Owner")]
public sealed class BucketOwner
{
    [XmlElement("ID")] public required string Id { get; set; }
    [XmlElement("DisplayName")] public required string DisplayName { get; set; }
}