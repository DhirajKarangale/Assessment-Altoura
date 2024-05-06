using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesManager : PersistentSingleton<AddressablesManager>
{
    [SerializeField] DataAssets dataAssets;

    internal void Load<T>(string assetName, System.Action<T> onLoadedCallback = null)
    {
        string path = dataAssets.GetPath(assetName);
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(path);

        handle.Completed += (op) =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                T loadedAsset = op.Result;
                onLoadedCallback?.Invoke(loadedAsset);
                Addressables.Release(op);
            }
            else
            {
                Debug.LogError("Failed to load asset: " + op.Status);
            }
        };
    }

    // internal void Load<T>(AssetReference assetReference, System.Action<T> onLoadedCallback = null)
    // {
    //     AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetReference);

    //     handle.Completed += (op) =>
    //     {
    //         if (op.Status == AsyncOperationStatus.Succeeded)
    //         {
    //             T loadedAsset = op.Result;
    //             onLoadedCallback?.Invoke(loadedAsset);
    //             Addressables.Release(op);
    //         }
    //         else
    //         {
    //             Debug.LogError("Failed to load asset: " + op.Status);
    //         }
    //     };
    // }
}