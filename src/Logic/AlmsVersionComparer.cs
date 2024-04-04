namespace ApertureMessenger.Logic;

/// <summary>
/// Allows comparing ALMS versions.
/// </summary>
public static class AlmsVersionComparer
{
    public enum Result
    {
        Older,
        Newer,
        Same
    }

    public static Result CompareAlmsVersions(string version1, string version2)
    {
        var version1Array = version1.Split('.');
        var version2Array = version2.Split('.');
        
        for (var i = 0; i < Math.Min(version1Array.Length, version2Array.Length); i++)
        {
            var v1Part = int.Parse(version1Array[i]);
            var v2Part = int.Parse(version2Array[i]);

            if (v1Part < v2Part) return Result.Older;
            if (v1Part > v2Part) return Result.Newer;
        }
        
        return Result.Same;
    }
}