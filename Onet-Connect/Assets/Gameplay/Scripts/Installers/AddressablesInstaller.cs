using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;
using Zenject;

namespace TGS.OnetConnect
{
    public class AddressablesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<AddressableLoader<GameObject>>().AsTransient();
            
            Container.Bind(typeof(AddressableLoader<>)).To(typeof(AddressableLoader<>)).AsTransient();
            
            Container.Bind<IFactory<string, Task<GameObject>>>()
                .To<AddressableFactory<GameObject>>()
                .AsTransient();
            Container.Bind<IFactory<string, Task<Texture2D>>>()
                .To<AddressableFactory<Texture2D>>()
                .AsTransient();
            Container.Bind<IFactory<string, Task<AudioClip>>>()
                .To<AddressableFactory<AudioClip>>()
                .AsTransient();
        }
    }

    public class AddressableLoader<T> where T : Object
    {
        public async Task<T> LoadAssetAsync(string address)
        {
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(address);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log($"Load asset at address Succeeded: {address}");

                return handle.Result;
            }
            else
            {
                Debug.LogError($"Failed to load asset at address: {address}");
                return null;
            }
        }
    }

    public class AddressableFactory<T> : IFactory<string, Task<T>> where T : Object
    {
        private readonly AddressableLoader<T> _loader;

        public AddressableFactory(AddressableLoader<T> loader)
        {
            _loader = loader;
        }

        public async Task<T> Create(string address)
        {
            return await _loader.LoadAssetAsync(address);
        }
    }
}