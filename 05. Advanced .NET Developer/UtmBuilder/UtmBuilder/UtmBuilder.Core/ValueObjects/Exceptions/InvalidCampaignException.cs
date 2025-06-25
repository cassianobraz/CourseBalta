using System.Text.RegularExpressions;

namespace UtmBuilder.Core.ValueObjects.Exceptions;

public partial class InvalidCampaignException : Exception
{
    private const string DefaultErrorMessage = "Invalid UTM parameters";

    public InvalidCampaignException(string message = DefaultErrorMessage)
        : base(message)
    {

    }

    public static void ThrowIfNull(string address, string message = DefaultErrorMessage)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new InvalidUrlException(message);

        if (!UrlRegex().IsMatch(address))
            throw new InvalidUrlException();
    }

    [GeneratedRegex(@"^https?:\/\/[^\s/$.?#].[^\s]*$")]
    private static partial Regex UrlRegex();
}