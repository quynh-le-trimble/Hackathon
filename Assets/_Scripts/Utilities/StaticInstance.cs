using UnityEngine;

namespace Hackathon
{
    /// <summary>
    /// A static instance is similar to a singleton, but instead of destroying any new instance, it overrides the current instance. 
    /// Useful for when Domain Reload is turned off: https://docs.unity3d.com/Manual/DomainReloading.html
    /// </summary>
    public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }
        protected virtual void Awake()
        {
            Instance = this as T;
        }
        protected virtual void OnApplicationQuit()
        {
            Instance = null;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// A basic singleton class. Similar to a static instance but will destroy any new versions created
    /// leaving the original.
    /// </summary>
    public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            base.Awake();
        }
    }

    /// <summary>
    /// A persistent singleton class. Will persist through scene loads.
    /// Use for system classes that require stateful, persistant data, 
    /// such as Audio Managers: requiring music to play between scene loads. 
    /// </summary>
    public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}
