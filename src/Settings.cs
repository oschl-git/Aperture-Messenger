namespace ApertureMessenger;

/// <summary>
/// Adjustable settings
/// </summary>
public static class Settings
{
    // The base ALMS API url Aperture Messenger should connect to
    public const string AlmsUrl = "https://alms.oschl.eu/";
    // public const string AlmsUrl = "http://127.0.0.1:5678/"; // for local testing
    
    // The ALMS version this version of Aperture Messenger targets
    public const string TargetAlmsVersion = "1.0.3";
    
    // How often should the screen/messages be refreshed
    public const int RefreshSleepSeconds = 2;
}