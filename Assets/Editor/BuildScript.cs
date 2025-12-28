using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;
using System.IO;

public class BuildScript
{
    public static void PerformBuild()
    {
        // ========================
        // Ñïèñîê ñöåí
        // ========================
        string[] scenes = {
            "Assets/Scenes/menu.unity",
            "Assets/Scenes/gameplay.unity",
            "Assets/Scenes/info.unity",
            "Assets/Scenes/end.unity",

        };

        // ========================
        // Ïóòè ê ôàéëàì ñáîðêè
        // ========================
        string aabPath = "ChickenRun.aab";
        string apkPath = "ChickenRun.apk";

        // ========================
        // Íàñòðîéêà Android Signing ÷åðåç ïåðåìåííûå îêðóæåíèÿ
        // ========================
        string keystoreBase64 = "+fCq9rAejFOeelT7AmUT+rpm9WdOvz3o3zDjhrViBXnA89QZ9T256ZRpRTpjFMMCcGCSqGSIb3DQEJFDEaHhgAYwBoAGkAYwBrAGUAbgBhAGwAaQBhAHMwIQYJKoZIhvcNAQkVMRQEElRpbWUgMTc2NjI1MzE4NjM1MjCCA9EGCSqGSIb3DQEHBqCCA8IwggO+AgEAMIIDtwYJKoZIhvcNAQcBMGYGCSqGSIb3DQEFDTBZMDgGCSqGSIb3DQEFDDArBBTfickg+M6isc3brfrHRcJyh5HsVwICJxACASAwDAYIKoZIhvcNAgkFADAdBglghkgBZQMEASoEEMe01y5DKhfI6tQfRqpEI4CAggNAFFVV+RHXwZmmgjNsXnfZgSOjNTt5rbfkqYuoRWY0hO2FgIaIPnHx3EuuLtg0mvZ68xzwCUjyucWVmezXCJGZLsWIhUzHh/AMp377nAeEuCxRwqNJtmDK4sOmA/f7XfYp9yBYeQVL+tFgm8vYxxMfvHrhrpvhg9zyoqCbbS99J9fBVKWjWrg+7CCYlW8aPxL+BOpXg2aoD2N3eZpMomvaia5zPsJ9SGdSSuD";
        string keystorePass = "strike";
        string keyAlias = "strike";
        string keyPass = "strike";

        string tempKeystorePath = null;

        if (!string.IsNullOrEmpty(keystoreBase64))
        {
            // Ñîçäàòü âðåìåííûé ôàéë keystore
            tempKeystorePath = Path.Combine(Path.GetTempPath(), "TempKeystore.jks");
            File.WriteAllBytes(tempKeystorePath, Convert.FromBase64String(keystoreBase64));

            PlayerSettings.Android.useCustomKeystore = true;
            PlayerSettings.Android.keystoreName = tempKeystorePath;
            PlayerSettings.Android.keystorePass = keystorePass;
            PlayerSettings.Android.keyaliasName = keyAlias;
            PlayerSettings.Android.keyaliasPass = keyPass;

            Debug.Log("Android signing configured from Base64 keystore.");
        }
        else
        {
            Debug.LogWarning("Keystore Base64 not set. APK/AAB will be unsigned.");
        }

        // ========================
        // Îáùèå ïàðàìåòðû ñáîðêè
        // ========================
        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        // ========================
        // 1. Ñáîðêà AAB
        // ========================
        EditorUserBuildSettings.buildAppBundle = true;
        options.locationPathName = aabPath;

        Debug.Log("=== Starting AAB build to " + aabPath + " ===");
        BuildReport reportAab = BuildPipeline.BuildPlayer(options);
        if (reportAab.summary.result == BuildResult.Succeeded)
            Debug.Log("AAB build succeeded! File: " + aabPath);
        else
            Debug.LogError("AAB build failed!");

        // ========================
        // 2. Ñáîðêà APK
        // ========================
        EditorUserBuildSettings.buildAppBundle = false;
        options.locationPathName = apkPath;

        Debug.Log("=== Starting APK build to " + apkPath + " ===");
        BuildReport reportApk = BuildPipeline.BuildPlayer(options);
        if (reportApk.summary.result == BuildResult.Succeeded)
            Debug.Log("APK build succeeded! File: " + apkPath);
        else
            Debug.LogError("APK build failed!");

        Debug.Log("=== Build script finished ===");

        // ========================
        // Óäàëåíèå âðåìåííîãî keystore
        // ========================
        if (!string.IsNullOrEmpty(tempKeystorePath) && File.Exists(tempKeystorePath))
        {
            File.Delete(tempKeystorePath);
            Debug.Log("Temporary keystore deleted.");
        }
    }
}