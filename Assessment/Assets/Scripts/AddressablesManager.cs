using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressablesManager : PersistentSingleton<AddressablesManager>
{
    internal void Load<T>(AssetReference assetReference, System.Action<T> onLoadedCallback = null)
    {
        AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetReference);

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
}