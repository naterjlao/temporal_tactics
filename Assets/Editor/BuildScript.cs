using UnityEditor;

public static class BuildScript
{
    // These must be updated on a project-by-project basis.
    private static string NAME = "TemporalTactics";
    private static string[] SCENES =
    {
        "Assets/_TemporalTactics/Working/Nate/Scenes/StartScreen.unity",
        "Assets/_TemporalTactics/Integrated/Scenes/Hello World Cube.unity"
    };
    /// @todo this probably can be autopopulated from the Build Settings file....

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

    [MenuItem("Custom Utilities/Build Windows")]
    public static void BuildWindows()
    {
        Build(SCENES, $"Builds/Windows/{NAME}.exe", BuildTarget.StandaloneWindows64);
    }

    [MenuItem("Custom Utilities/Build Linux")]
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
