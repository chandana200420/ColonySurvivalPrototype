using System.IO;
using UnityEngine;

public static class JsonLoader
{
    public static T Load<T>(string filename)
    {
        string path = Path.Combine(
            Application.streamingAssetsPath,
            filename);

        if (!File.Exists(path))
        {
            Debug.LogError($"JSON file not found: {path}");
            return default;
        }

        string json = File.ReadAllText(path);

        return JsonUtility.FromJson<T>(json);
    }
}