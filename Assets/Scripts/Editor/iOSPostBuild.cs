using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

public sealed class iOSPostBuild
{
    [PostProcessBuildAttribute]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        switch (target)
        {
            case BuildTarget.iOS:
                disableBitcode(pathToBuiltProject);
                break;
            default: break;
        }
    }

    private static void disableBitcode(string pathToBuiltProject)
    {
        var project = new PBXProject();
        var pbxPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
        project.ReadFromFile(pbxPath);

        project.SetBuildProperty(project.GetUnityFrameworkTargetGuid(), "ENABLE_BITCODE", "NO"); 
        project.SetBuildProperty(project.GetUnityMainTargetGuid(), "ENABLE_BITCODE", "NO");

        project.WriteToFile(pbxPath);
    }
}