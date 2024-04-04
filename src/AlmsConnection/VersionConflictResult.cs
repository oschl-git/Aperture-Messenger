using ApertureMessenger.Logic;

namespace ApertureMessenger.AlmsConnection;

public class VersionConflictResult
{
    public string TargetVersion;
    public string ActualVersion;
    public AlmsVersionComparer.Result Result;

    public VersionConflictResult(string targetVersion, string actualVersion)
    {
        TargetVersion = targetVersion;
        ActualVersion = actualVersion;
        Result = AlmsVersionComparer.CompareAlmsVersions(targetVersion, actualVersion);
    }
}