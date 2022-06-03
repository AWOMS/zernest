namespace AWOMS.Zernest.Models;

public sealed class Settings
{
    public const string Position = "Settings";
    public string ApplicationName { get; set; } = string.Empty;
    public string ApplicationVersion { get; set; } = string.Empty;
    public string ApplicationUrl { get; set; } = string.Empty;
    public string ZedGraphQLUrl { get; set; } = string.Empty;
    public bool EnforceOutboundIpCheck { get; set; } = false;
    public string OutboundIpSource { get; set; } = string.Empty;
    public string[] AllowedOutboundIpAddresses { get; set; } = new string[] {};
}