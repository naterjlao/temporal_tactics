using UnityEditor;

public static class BuildScript
{
    // These must be updated on a project-by-project basis.
    private static string NAME = "TemporalTactics";
    private static string[] SCENES =
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
        Build(SCENES, $"Builds/Windows/{NAME}.exe", BuildTarget.StandaloneWindows64);
    }

    public static void BuildMacOSX()
    {
        Build(SCENES, $"Builds/MacOSX/{NAME}", BuildTarget.StandaloneOSX);
    }

    public static void BuildAll()
    {
        BuildWindows();
        BuildMacOSX();
    }
}
