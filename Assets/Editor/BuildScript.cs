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
        string keystoreBase64 ="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";
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