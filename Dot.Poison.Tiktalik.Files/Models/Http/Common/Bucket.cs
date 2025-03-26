namespace Dot.Poison.Tiktalik.Files.Models.Http.Common;

[XmlRoot("Bucket")]
public sealed class Bucket
{
    [XmlElement("Name")] public string Name { get; set; }

    [XmlElement("CreationDate")] public DateTime CreationDate { get; set; }
}