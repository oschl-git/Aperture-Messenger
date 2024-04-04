using ApertureMessenger.Logic;

namespace ApertureMessenger.AlmsConnection.Objects;

public class VersionConflictResult
{
    public readonly string TargetVersion;
    public readonly string ActualVersion;
    public readonly AlmsVersionComparer.Result Result;

    public VersionConflictResult(string targetVersion, string actualVersion, AlmsVersionComparer.Result result)
    {
        TargetVersion = targetVersion;
        ActualVersion = actualVersion;
        Result = result;
    }
}