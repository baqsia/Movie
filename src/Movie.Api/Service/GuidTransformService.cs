namespace Movie.Api.Service;

public interface IGuidTransformService
{
    string ToUriString(Guid guid);
    Guid FromUriString(string guidString);
}

public class GuidTransformService : IGuidTransformService
{
    public string ToUriString(Guid guid)
    {
        return Convert.ToBase64String(guid.ToByteArray())
            .Replace("=", string.Empty)
            .Replace("/", "_");
    }

    public Guid FromUriString(string guidString)
    {
        if (!guidString.EndsWith("=="))
        {
            guidString = $"{guidString}==";
        }

        guidString = guidString.Replace("_", "/");

        var base64 = Convert.FromBase64String(guidString);
        return new Guid(base64);
    }
}