using UnityEngine;

namespace Base.Singleton
{
    public abstract class SingletonPersistent<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        Debug.Log("Create new singleton persistent: " + typeof(T).ToString().Color("yellow") + " in scene");
                        var go = new GameObject { name = typeof(T).Name };
                        _instance = go.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
                OnAwake();
            }
            else
            {
                Debug.LogWarning("Destroy duplicate singleton: " + typeof(T).ToString().Color("red"));
                Destroy(gameObject);
            }
        }

        protected abstract void OnAwake();
    }
}