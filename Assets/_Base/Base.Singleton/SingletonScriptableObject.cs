using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Base.Singleton
{
    public class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        private const string folderPath = "Assets/Resources";
        private const string fileExtension = ".asset";

        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var className = typeof(T).Name;
                    _instance = Resources.Load<T>(className);

                    if (_instance == null)
                    {
                        var fileName = typeof(T).Name;
#if UNITY_EDITOR
                        // create folder & scriptable object
                        Directory.CreateDirectory(folderPath);
                        AssetDatabase.CreateAsset(
                            _instance = CreateInstance<T>(),
                            Path.Combine(folderPath, fileName + fileExtension)
                        );
                        AssetDatabase.SaveAssets();
                        Debug.Log("Created " + fileName.Color("yellow") + " in " + folderPath.Color("cyan"));
#else
                        Debug.LogError("Can't find " + fileName.Color("yellow") + " in " + folderPath.Color("cyan"));
#endif
                    }
                }

#if UNITY_EDITOR
                Selection.activeObject = _instance;
#endif

                return _instance;
            }
        }
    }
}