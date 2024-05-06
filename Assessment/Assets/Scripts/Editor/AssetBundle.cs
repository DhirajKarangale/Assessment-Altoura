using System;
using UnityEditor;
using UnityEngine;

public class AssetBundle
{
    [MenuItem("Assets/Build Assets Bundle")]
    private static void Build()
    {
        string path = Application.dataPath + "/Bundles";
        try
        {
            BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        }
        catch (Exception exception)
        {
            Debug.Log(exception);
        }
    }
}