using UnityEngine;

namespace Scripts
{
    public abstract class SingletonPattern<T> : MonoBehaviour where T : SingletonPattern<T>
    {
        private static T instance;
        public static T Instance { get => instance; }

        void Awake()
        {
            if (instance == null)
                instance = (T)this;
            else
                Destroy(gameObject);
        }
    }
}