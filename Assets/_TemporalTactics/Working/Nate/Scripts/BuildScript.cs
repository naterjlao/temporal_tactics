using UnityEditor;

public static class BuildScript
{
    // These must be updated on a project-by-project basis.
    private static string name = "TemporalTactics";
    private static string[] scenes =
    {
        "Assets/_TemporalTactics/Integrated/Scenes/Hello World Cube.unity"
    };

    private static void Build(string[] scenes, string locationPathName, BuildTarget target)
    {
        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = locationPathName,
            target = target
        };
        BuildPipeline.BuildPlayer(options);
    }

    public static void BuildWindows()
    {
        Build(scenes, $"Builds/Windows/{name}.exe", BuildTarget.StandaloneWindows64);
    }

    public static void BuildMacOSX()
    {
        Build(scenes, $"Builds/MacOSX/{name}", BuildTarget.StandaloneOSX);
    }

    public static void BuildAll()
    {
        BuildWindows();
        BuildMacOSX();
    }
}
