using UnityEngine;

namespace GCTools
{
    public abstract class CreateSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        private static T initInstance;

        private static bool isDestroy;

        public static T Instance
        {
            get
            {
                if (isDestroy)
                    return null;

                if (instance == null)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null && Application.isPlaying &&!isDestroy)
                    {
                        var obj = new GameObject(typeof(T).Name);
                        instance = obj.AddComponent<T>();
                    }
                }

                InitInstance();

                return instance;
            }
        }

        private static void InitInstance()
        {
            if (initInstance == null && instance != null && Application.isPlaying)
            {
                if(Application.isPlaying)
                    DontDestroyOnLoad(instance.gameObject);

                var s = instance as CreateSingleton<T>;
                s.InitSingleton();

                initInstance = instance;
            }
        }

        public static bool IsInstance()
        {
            return instance != null && isDestroy == false;
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                InitInstance();
            }
            else
            {
                var s = instance as CreateSingleton<T>;
                s.DuplicateDetection(this as T);

                //Destroy(this.gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
                isDestroy = true;
            }
        }

        protected virtual void DuplicateDetection(T duplicate) { }

        protected abstract void InitSingleton();
    }
}
