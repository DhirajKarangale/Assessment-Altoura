using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AssetData", menuName = "Data/AssetData")]
public class DataAssets : ScriptableObject
{
    public List<Data> assets = new List<Data>();

    public string GetPath(string assetName)
    {
        foreach (Data data in assets)
        {
            if (data.name == assetName) return data.path;
        }

        return null;
    }
}

[System.Serializable]
public class Data
{
    public string name;
    public string path;
}