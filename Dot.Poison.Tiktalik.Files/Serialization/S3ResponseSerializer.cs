namespace Dot.Poison.Tiktalik.Files.Serialization;

public class S3ResponseSerializer
{
    #region Singleton

    private S3ResponseSerializer() { }
    
    private static S3ResponseSerializer? _instance;
    
    /// <summary>
    /// Singleton implementation used to allow extension methods
    /// </summary>
    public static S3ResponseSerializer Instance => _instance ??= new S3ResponseSerializer();
    
    #endregion

    public TResult Deserialize<TResult>(string xml) where TResult : class
    {
        using StringReader reader = new(xml);
        XmlSerializer serializer = new(typeof(TResult));
        return (serializer.Deserialize(reader) as TResult)!;
    }

    public TResult Deserialize<TResult>(Stream stream) where TResult : class
    {
        XmlSerializer serializer = new(typeof(TResult));
        return (serializer.Deserialize(stream) as TResult)!;
    }
}