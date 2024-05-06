using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            string assetBundleDirectory = Application.dataPath + "/Bundles";

            // Load the asset bundle
            AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundleDirectory + "/cb");

            // Example: Load a prefab from the asset bundle
            GameObject prefab = assetBundle.LoadAsset<GameObject>("Cube");

            // Instantiate the loaded prefab
            Instantiate(prefab);

            // Don't forget to unload the asset bundle when you're done
            assetBundle.Unload(false);
        }
    }
}
