namespace AnyStatus.Apps.Windows.Features.Endpoints;

internal class AccessTokenResponse
{
    public bool Success { get; set; }

    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
}