using UnityEngine;

namespace RaiNa.Unity.IO
{
    public static class UnityPath
    {
        public static string DataPath => Application.dataPath;
        public static string PersistentDataPath => Application.persistentDataPath;
        public static string StreamingAssetsDataPath => Application.streamingAssetsPath;
        public static string TempCachePath => Application.temporaryCachePath;
    }
}
